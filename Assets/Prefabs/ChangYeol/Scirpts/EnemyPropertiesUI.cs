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
        public ListSpawnManager spawnManager;

        ListWaveData ListWaveData;
        #endregion
        public void ShowProUI()
        {
            StartCoroutine(HideProUI());
            ListWaveData = spawnManager.waves[spawnManager.waveCount];
            foreach (var enemy in ListWaveData.enemies)
            {
                float maxhealth = enemy.enemyPrefab.GetComponent<Health>().maxHealth;
                maxhealth *= (spawnManager.waveCount + 1);
                GameObject info = Instantiate(Enemyinfo,EnemyProUI.transform);
                Upgrade enemyinfo = info.GetComponent<EnemyInfo>().EnemyText;
                enemyinfo.name.text = enemy.enemyPrefab.name;
                enemyinfo.image.sprite = enemy.enemyPrefab.GetComponent<EnemyController>().sprite;
                enemyinfo.Hp.text = "HP : " + maxhealth.ToString();
                enemyinfo.Mp.text = "Armor : " + enemy.enemyPrefab.GetComponent<Health>().baseArmor.ToString();
                enemyinfo.Attack.text = "Attack : " + enemy.enemyPrefab.GetComponent<EnemyAttackController>().baseAttackDamage.ToString();
                enemyinfo.AttackSpeed.text = "AttackSpeed : " + enemy.enemyPrefab.GetComponent<EnemyAttackController>().baseAttackDelay.ToString();
                enemyinfo.Buycost.text = " : " + enemy.enemyPrefab.GetComponent<EnemyController>().rewardGoldCount.ToString();
                enemyinfo.UpgradeMoney.text = "X" + enemy.count.ToString();
                Destroy(info,waitSecond);
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