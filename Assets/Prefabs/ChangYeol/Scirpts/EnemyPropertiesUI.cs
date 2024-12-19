using Defend.Enemy;
using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.UI
{
    public class EnemyPropertiesUI : MonoBehaviour
    {
        #region Variables
        public GameObject EnemyProUI;
        //Enemy ¼Ó¼º°ª
        public GameObject Enemyinfo;

        public float waitSecond = 5;
        private BuildManager buildManager;
        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
            //ShowProUI();
        }
        public void ShowProUI()
        {
            StartCoroutine(HideProUI());
            ListWaveData waveData = buildManager.spawnManager.waves[buildManager.spawnManager.waveCount];

            foreach (var enemy in waveData.enemies)
            {
                GameObject info = Instantiate(Enemyinfo,EnemyProUI.transform);
                Upgrade enemyinfo = Enemyinfo.GetComponent<EnemyInfo>().EnemyText;
                enemyinfo.name.text = enemy.enemyPrefab.name;
                enemyinfo.image.sprite = enemy.enemyPrefab.GetComponent<EnemyController>().sprite;
                enemyinfo.Hp.text = "HP : " + enemy.enemyPrefab.GetComponent<Health>().maxHealth.ToString();
                enemyinfo.Mp.text = "Armor : " + enemy.enemyPrefab.GetComponent<Health>().baseArmor.ToString();
                enemyinfo.Attack.text = "Attack : " + enemy.enemyPrefab.GetComponent<EnemyAttackController>().baseAttackDamage.ToString();
                enemyinfo.AttackSpeed.text = "AttackSpeed : " + enemy.enemyPrefab.GetComponent<EnemyAttackController>().baseAttackDelay.ToString();
                enemyinfo.Buycost.text = " : " + enemy.enemyPrefab.GetComponent<EnemyController>().rewardGoldCount.ToString();
                enemyinfo.UpgradeMoney.text = "X" + enemy.count.ToString();
            }
        }
        IEnumerator HideProUI()
        {
            EnemyProUI.SetActive(true);
            yield return new WaitForSeconds(waitSecond);
            EnemyProUI.SetActive(false);
        }
    }
}