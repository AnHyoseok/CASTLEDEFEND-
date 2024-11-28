using UnityEngine;

namespace Defend.Enemy
{
    /// <summary>
    /// 적의 공격을 담당하는 컨트롤러 클래스
    /// </summary>
    public class EnemyAttackController : MonoBehaviour
    {
        // 공격 대상
        [SerializeField] private Transform attackTarget;

        private EnemyStats enemyStats; // 적의 기본 속성
        private Animator animator; // 애니메이터 컴포넌트

        //공격 관련
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private float attackCooldown;
        private bool isAttacking;

        private void Start()
        {
            // 참조
            enemyStats = GetComponent<EnemyStats>();
            animator = GetComponent<Animator>();

            //초기화
            CurrentAttackDamage = baseAttackDamage;
            CurrentAttackDelay = baseAttackDelay;
            attackCooldown = 0f;
            isAttacking = false;
        }

        private void Update()
        {
            //if (isAttacking || !attackTarget) return;
            if (isAttacking) return;
            // 공격 쿨타임마다 공격
            if (attackCooldown > 0f)
            {
                attackCooldown -= Time.deltaTime;
            }
            else
            {
                TriggerAttackAnimation();
            }

        }

        private void TriggerAttackAnimation()
        {
            // 공격 애니메이션 실행
            Debug.Log("Attack!");
            animator.SetTrigger("Attack");
            isAttacking = true;
        }

        // 애니메이션 이벤트에서 호출할 메서드
        public void PerformAttack()
        {
            if (attackTarget == null) return;

            //// 데미지 계산 및 적용
            //IDamageable damageableTarget = attackTarget.GetComponent<IDamageable>();
            //if (damageableTarget != null)
            //{
            //    damageableTarget.TakeDamage(CurrentAttackDamage);
            //    Debug.Log($"{enemyStats.type} dealt {CurrentAttackDamage} damage to the target.");
            //}
        }

        // 공격 대상을 설정하는 메서드
        //public void SetAttackTarget(Transform target)
        //{
        //    attackTarget = target;
        //}

        public void StartAttackCooldown()
        {
            isAttacking = false;
            attackCooldown = CurrentAttackDelay; // 공격 대기시간 초기화
        }
    }
}
