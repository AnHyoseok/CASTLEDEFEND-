using Defend.TestScript;
using Defend.Utillity;

using UnityEngine;
using UnityEngine.Events;

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

        private EnemyMoveController moveController; // 적의 이동 속성
        private Animator animator; // 애니메이터 컴포넌트
        private Health damageableTarget;


        //공격 관련
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private bool isAttacking = false;
        private bool hasArrived = false;

        private EnemyController enemyController;
        private bool isChanneling = false;

        public UnityAction<float> AttackDamageChanged;
        public UnityAction OnAttacking;

        //공격시 SFX
        [SerializeField] private AudioClip attackClip;
        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle>().transform;
            damageableTarget = attackTarget.GetComponent<Health>();
            // 참조
            moveController = GetComponent<EnemyMoveController>();
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();

            //초기화
            CurrentAttackDamage = baseAttackDamage;
            //도착하자마자 공격
            CurrentAttackDelay = 0;
        }

        private void Start()
        {
            moveController.EnemyArrive += OnEnemyArrive;
            enemyController.OnChanneling += OnChanneling;
        }


        private void Update()
        {
            //버프몬스터는 공격기능 제외
            if (enemyController.type == EnemyType.Buffer) return;

            //Enemy가 마지막 WayPoint에 도착하지 않았거나, 공격중이거나, 공격 타겟이 없거나, 스킬을 사용중이라면 공격 딜레이 시간이 감소하지 않고 공격도 하지 않음
            if (!hasArrived || isAttacking || !damageableTarget || isChanneling) return;
            // 공격 쿨타임마다 공격
            if (CurrentAttackDelay > 0f)
            {
                CurrentAttackDelay -= Time.deltaTime;
            }
            else
            {
                TriggerAttackAnimation();
            }
        }

        private void TriggerAttackAnimation()
        {
            if (damageableTarget.CurrentHealth > 0f)
            {
                transform.LookAt(attackTarget);
                // 공격 애니메이션 실행
                animator.SetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            }
            else
            {
                animator.ResetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            }
        }

        public void ChangeAttackingStatus()
        {
            isAttacking = !isAttacking;
            OnAttacking?.Invoke();
        }

        // 애니메이션 이벤트에서 호출할 메서드
        public void PerformAttack()
        {
            AudioUtility.CreateSFX(attackClip, transform.position, AudioUtility.AudioGroups.EFFECT, 1);
            damageableTarget.TakeDamage(CurrentAttackDamage);
        }

        public void StartAttackCooldown()
        {
            CurrentAttackDelay = baseAttackDelay; // 공격 대기시간 초기화
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
