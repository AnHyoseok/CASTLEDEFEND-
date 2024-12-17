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
        public GameObject[] enemylist;
        //Enemy ¼Ó¼º°ª
        public Upgrade[] EnemyText;
        private BuildManager buildManager;
        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
            //ShowProUI();
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
                EnemyText[i].Buycost.text = " : " + buildManager.enemyInfo[i].Money.rewardGoldCount.ToString();
            }
        }
        public void HideProUI()
        {
            EnemyProUI.SetActive(false);
        }
    }
}