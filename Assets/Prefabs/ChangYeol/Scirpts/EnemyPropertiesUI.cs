using Defend.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class EnemyPropertiesUI : MonoBehaviour
    {
        #region Variables
        public GameObject EnemyProUI;
        //Enemy 속성값
        public Upgrade[] EnemyText;
        private BuildManager buildManager;

        public GameObject[] targetObject; // 크기에 따라 조절할 오브젝트
        public RectTransform canvasRect;
        public RectTransform[] targetRect;

        [HideInInspector]public bool iswarrior = false;
        [HideInInspector]public bool iswizard = false;
        [HideInInspector]public bool isBoss = false;
        private Vector2 previousSize; // 이전 프레임의 캔버스 크기
        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
            ShowProUI();
            previousSize = canvasRect.sizeDelta;
            iswarrior = targetObject[0].activeSelf;
            iswizard = targetObject[1].activeSelf;
            isBoss = targetObject[2].activeSelf;
        }
        private void Update()
        {
            if (iswarrior || iswizard || isBoss)
            {
                CalculateCanvasSize();
            }
        }
        public void ShowProUI()
        {
            EnemyProUI.SetActive(true);
            for ( int i = 0; i < EnemyText.Length; i++ )
            {
                EnemyText[i].name.text = buildManager.enemyInfo[i].Enemyname;
                EnemyText[i].image.sprite = buildManager.enemyInfo[i].enemySprite;
                EnemyText[i].Hp.text = "HP : " + buildManager.enemyInfo[i].Health.maxHealth.ToString();
                EnemyText[i].Mp.text = "Armor : " + buildManager.enemyInfo[i].Health.baseArmor.ToString();
                EnemyText[i].Attack.text = "Attack : " + buildManager.enemyInfo[i].Attack.baseAttackDamage.ToString();
                EnemyText[i].AttackSpeed.text = "AttackSpeed : " + buildManager.enemyInfo[i].Attack.baseAttackDelay.ToString();
                EnemyText[i].Buycost.text = "Get Money : " + buildManager.enemyInfo[i].Money.rewardGoldCount.ToString();
            }
        }
        public void HideProUI()
        {
            EnemyProUI.SetActive(false);
        }
        void CalculateCanvasSize()
        {
            for ( int i = 0;i < targetObject.Length;i++ )
            {
                switch (targetObject[i].activeSelf)
                {
                    case true:
                        canvasRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x, previousSize.y + canvasRect.sizeDelta.y);
                        previousSize = canvasRect.sizeDelta;
                        break;
                    case false:
                        canvasRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x, previousSize.y - canvasRect.sizeDelta.y);
                        previousSize = canvasRect.sizeDelta;
                        break;
                }
            }
        }
    }
}