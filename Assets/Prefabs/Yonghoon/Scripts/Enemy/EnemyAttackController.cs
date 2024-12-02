using Defend.TestScript;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.XR.OpenXR.Features.Interactions.HTCViveControllerProfile;

namespace Defend.Enemy
{
    /// <summary>
    /// 적의 공격을 담당하는 컨트롤러 클래스
    /// </summary>
    public enum EnemyType
    {
        Buffer,
        Warrior,
        Tanker,
        Boss
    }
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

        private float attackCooldown;
        private bool isAttacking;
        private bool hasArrived;

        public UnityAction<float> AttackDamageChanged;
        public UnityAction<float> AttackDelayChanged;

        public EnemyType type;
        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle1>().transform;

            // 참조
            moveController = GetComponent<EnemyMoveController>();
            animator = GetComponent<Animator>();

            //초기화
            CurrentAttackDamage = baseAttackDamage;
            CurrentAttackDelay = baseAttackDelay;
            attackCooldown = 0f;
            isAttacking = false;
            hasArrived = false;

            moveController.EnemyArrive += OnEnemyArrive;
        }

        private void Update()
        {
            if (!hasArrived) return;
            if (isAttacking || !attackTarget) return;
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
            animator.SetTrigger("Attack");
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

        public void ChangedAttackDelay(float amount)
        {
            CurrentAttackDelay = Mathf.Max(CurrentAttackDelay - amount, 0.5f);
            AttackDelayChanged?.Invoke(amount);
        }

        private void OnEnemyArrive()
        {
            hasArrived = true;
        }
    }
}
