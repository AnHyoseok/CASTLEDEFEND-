using UnityEngine;
using UnityEngine.Events;

namespace Defend.TestScript
{
    /// <summary>
    /// ü���� �����ϴ� Ŭ����
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Variables

        //ü�°���
        [SerializeField] private float maxHealth = 100f;    //�ִ� Hp
        public float CurrentHealth { get; private set; }    //���� Hp

        //�Ƹ� ����
        [SerializeField] private float baseArmor = 5f;
        public float CurrentArmor { get; private set; }

        private bool isDeath = false;                       //���� üũ

        //UnityAction
        public UnityAction<float> OnDamaged;
        public UnityAction OnDie;
        public UnityAction<float> OnHeal;

        public float GetRatio() => CurrentHealth / maxHealth;

        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CurrentHealth = maxHealth;
            CurrentArmor = baseArmor;
        }

        //��
        public void Heal(float amount)
        {
            float beforeHealth = CurrentHealth;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

            //real Heal ���ϱ�
            float realHeal = CurrentHealth - beforeHealth;
            if (realHeal > 0f)
            {
                //�� ����
                OnHeal?.Invoke(realHeal);
            }
        }

        //damageSource: �������� �ִ� ��ü
        public void TakeDamage(float damage)
        {
            // ���� ���� �� ���� ������ ���
            float mitigatedDamage = Mathf.Max(damage - CurrentArmor, 0); // Clamp ��� Max ��� (�� ����)

            // ���������� ���� ������ ���
            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

            if (realDamage > 0f)
            {
                // ü�� ����, ����������Ʈ ����
                CurrentHealth -= realDamage;          
                OnDamaged?.Invoke(realDamage);
            }

            // ü���� 0 ���϶�� ���� ó��
            if (CurrentHealth <= 0f)
            {
                HandleDeath();
            }
        }

        //���� ó�� ����
        void HandleDeath()
        {
            //���� üũ
            if (isDeath)
                return;

            if (CurrentHealth <= 0f)
            {
                isDeath = true;

                //���� ����
                OnDie?.Invoke();
            }
        }
    }
}