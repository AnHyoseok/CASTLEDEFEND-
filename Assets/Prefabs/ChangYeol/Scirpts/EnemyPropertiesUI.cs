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

        public Upgrade EnemyText;

        public Transform head;

        public EnemyState enemyStats;

        private BuildManager buildManager;

        [SerializeField] private float distance = 1.5f;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            
        }
        public void ShowProUI(EnemyState enemy)
        {
            //선택한 적 저장
            enemyStats = enemy;
            //적 속성 UI 오른쪽 나온다(버그 뒤로 돌면 왼쪽으로 나온다)
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;

            if (enemyStats)
            {
                /*EnemyText.name.text = enemyStats.name;
                EnemyText.Hp.text = "Hp : " + enemyStats.baseHealth.ToString();
                EnemyText.Mp.text = "Armor : " + enemyStats.baseArmor.ToString();
                EnemyText.Attack.text = "Attack : " + enemyStats.baseAttackDamage.ToString();
                EnemyText.AttackSpeed.text = "AttackSpeed : " + enemyStats.baseSpeed.ToString();
                EnemyText.Sellcost.text = "Get Money : " + enemyStats.rewardGold.ToString();*/
            }
        }
    }
}