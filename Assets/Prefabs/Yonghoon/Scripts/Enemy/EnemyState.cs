using Defend.TestScript;
using UnityEngine;

/// <summary>
/// Enemy의 현재정보를 정의하는 클래스
/// </summary>
namespace Defend.Enemy
{
    public class EnemyState : MonoBehaviour
    {
        //이동관련
        public float enemySpeed;

        //공격 관련
        public float enemyAttackDamage;
        public float enemyAttackDelay;

        //체력관련
        public float enemyHealth;

        //방어관련
        public float enemyArmor;

        //적 타입
        public EnemyType type;

        //참조를 받아올 변수
        private Health health;
        private EnemyMoveController moveController;
        private EnemyAttackController attackController;

        private void Start()
        {
            //참조
            health = GetComponent<Health>();
            moveController = GetComponent<EnemyMoveController>();
            attackController = GetComponent<EnemyAttackController>();

            //변화를 줄 UnityActions
            //Health 변경
            health.OnDamaged += ChangeHealth;
            health.OnHeal += ChangeHealth;

            //Armor 변경
            health.Armorchange += ChangeArmor;

            //공격 능력치 변경
            attackController.AttackDamageChanged += ChangeDamage;
            attackController.AttackDelayChanged += ChangeDelay;

            //이동속도 변경
            moveController.MoveSpeedChanged += ChangeSpeed;

            //초기화
            enemyHealth = health.CurrentHealth;

            enemyArmor = health.CurrentArmor;

            enemyAttackDamage = attackController.CurrentAttackDamage;
            enemyAttackDelay = attackController.CurrentAttackDelay;
            type = attackController.type;

            enemySpeed = moveController.CurrentSpeed;
            
        }

        private void ChangeDelay(float amount)
        {
            enemyAttackDelay -= amount;
        }

        private void ChangeDamage(float amount)
        {
            enemyAttackDamage += amount;
        }

        private void ChangeArmor(float amount)
        {
            enemyArmor += amount;
        }

        private void ChangeHealth(float amount)
        {
            enemyHealth += amount;
        }

        private void ChangeSpeed(float value)
        {
            enemySpeed = value;
        }
    }
}