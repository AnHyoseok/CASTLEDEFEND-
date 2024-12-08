using Defend.TestScript;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

namespace Defend.Enemy
{
    /// <summary>
    /// 적의 공격을 담당하는 컨트롤러 클래스
    /// </summary>
    public class EnemyAttackController : MonoBehaviour
    {
        #region Variables
        // 공격 대상
        [SerializeField] private Transform attackTarget;

        private EnemyMoveController moveController; // 적의 기본 속성
        private Animator animator; // 애니메이터 컴포넌트

        //공격 관련
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private float attackCooldown = 0f;
        private bool isAttacking = false;
        private bool hasArrived = false;

        private EnemyController enemyController;
        private bool isChanneling = false;

        public UnityAction<float> AttackDamageChanged;
        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle>().transform;

            // 참조
            moveController = GetComponent<EnemyMoveController>();
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();

            //초기화
            CurrentAttackDamage = baseAttackDamage;
            CurrentAttackDelay = baseAttackDelay;
        }

        private void Start()
        {
            moveController.EnemyArrive += OnEnemyArrive;
            enemyController.OnChanneling += OnChanneling;
        }


        private void Update()
        {
            if (!hasArrived) return;
            if (isAttacking || !attackTarget || isChanneling) return;
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
            animator.SetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            isAttacking = true;
        }

        // 애니메이션 이벤트에서 호출할 메서드
        public void PerformAttack()
        {
            // 데미지 계산 및 적용
            Health damageableTarget = attackTarget.GetComponent<Health>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(CurrentAttackDamage);
            }
        }

        public void StartAttackCooldown()
        {
            isAttacking = false;
            attackCooldown = CurrentAttackDelay; // 공격 대기시간 초기화
        }

        public void ChangedAttackDamage(float amount)
        {
            CurrentAttackDamage = Mathf.Max(CurrentAttackDamage + amount, 1f);
            AttackDamageChanged?.Invoke(amount);
        }

        private void OnEnemyArrive()
        {
            hasArrived = true;
        }

        private void OnChanneling()
        {
            isChanneling = !isChanneling;
        }
    }
}
