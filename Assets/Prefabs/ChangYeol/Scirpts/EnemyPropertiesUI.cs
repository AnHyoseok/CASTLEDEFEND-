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

        private EnemyXRSimple xRSimple;

        private BuildManager buildManager;

        [SerializeField] private float distance = 1.5f;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            
        }
        public void ShowProUI(EnemyXRSimple enemy)
        {
            //선택한 적 저장
            xRSimple = enemy;
            //적 속성 UI 오른쪽 나온다(버그 뒤로 돌면 왼쪽으로 나온다)
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            Debug.Log(enemy.enemyState.enemyHealth.ToString());
            EnemyText.name.text = enemy.enemyState.type.ToString();
            EnemyText.Hp.text = "Hp : " + enemy.enemyState.enemyHealth.ToString();
            EnemyText.Mp.text = "Armor : " + enemy.enemyState.enemyArmor.ToString();
            EnemyText.Attack.text = "Attack : " + enemy.enemyState.enemyAttackDamage.ToString();
            EnemyText.AttackSpeed.text = "AttackSpeed : " + enemy.enemyState.enemySpeed.ToString();
            //EnemyText.Sellcost.text = "Get Money : " + enemy.enemyState..ToString();
            if (xRSimple)
            {
                
            }
            EnemyProUI.SetActive(true);
        }
        public void HideProUI()
        {
            EnemyProUI.SetActive(false);

            xRSimple = null;
        }
    }
}