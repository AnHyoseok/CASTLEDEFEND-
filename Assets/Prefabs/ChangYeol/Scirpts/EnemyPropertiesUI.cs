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
        RectTransform[] targetRect;
        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
            ShowProUI();
        }
        private void Update()
        {
            /*for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject != null && targetObject[i].activeSelf)
                {
                    targetRect = targetObject[i].GetComponents<RectTransform>();
                    canvasRect.sizeDelta = new Vector2(canvasRect.sizeDelta.x, canvasRect.sizeDelta.y + targetRect[0].sizeDelta.y);
                }
            }*/
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
    }
}