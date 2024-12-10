using Defend.TestScript;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.Events;

namespace Defend.Enemy
{
    /// <summary>
    /// ���� ������ ����ϴ� ��Ʈ�ѷ� Ŭ����
    /// </summary>
    public class EnemyAttackController : MonoBehaviour
    {
        #region Variables
        // ���� ���
        [SerializeField] private Transform attackTarget;

        private EnemyMoveController moveController; // ���� �⺻ �Ӽ�
        private Animator animator; // �ִϸ����� ������Ʈ

        //���� ����
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        //������� ���ݵ������� �ʿ���
        private EnemyType enemyType;

        public float CurrentAttackDamage { get; private set; }
        public float CurrentAttackDelay { get; private set; }

        private bool isAttacking = false;
        private bool hasArrived = false;

        private EnemyController enemyController;
        private bool isChanneling = false;

        public UnityAction<float> AttackDamageChanged;


        #endregion

        private void Awake()
        {
            attackTarget = FindAnyObjectByType<HealthBasedCastle>().transform;

            // ����
            moveController = GetComponent<EnemyMoveController>();
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();

            //�ʱ�ȭ
            CurrentAttackDamage = baseAttackDamage;
            //�������ڸ��� ����
            CurrentAttackDelay = 0;
        }

        private void Start()
        {
            moveController.EnemyArrive += OnEnemyArrive;
            enemyController.OnChanneling += OnChanneling;
            enemyType = enemyController.type;
        }


        private void Update()
        {
            //�������ʹ� ���ݱ�� ����
            if (enemyType == EnemyType.Buffer) return;

            //Enemy�� ������ WayPoint�� �������� �ʾҰų�, �������̰ų�, ���� Ÿ���� ���ų�, ��ų�� ������̶�� ���� ������ �ð��� �������� �ʰ� ���ݵ� ���� ����
            if (!hasArrived || isAttacking || !attackTarget || isChanneling) return;
            // ���� ��Ÿ�Ӹ��� ����
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
            // ���� �ִϸ��̼� ����
            animator.SetTrigger(Constants.ENEMY_ANIM_ATTACKTRIGGER);
            isAttacking = true;
        }

        // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
        public void PerformAttack()
        {
            // ������ ��� �� ����
            Health damageableTarget = attackTarget.GetComponent<Health>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(CurrentAttackDamage);
            }
        }

        public void StartAttackCooldown()
        {
            isAttacking = false;
            CurrentAttackDelay = baseAttackDelay; // ���� ���ð� �ʱ�ȭ
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
