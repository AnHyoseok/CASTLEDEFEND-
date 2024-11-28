using UnityEngine;
using UnityEngine.Events;

namespace Defend.TestScript
{
    /// <summary>
    /// 체력을 관리하는 클래스
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Variables

        //체력관련
        [SerializeField] private float maxHealth = 100f;    //최대 Hp
        public float CurrentHealth { get; private set; }    //현재 Hp

        //아머 관련
        [SerializeField] private float baseArmor = 5f;
        public float CurrentArmor { get; private set; }

        private bool isDeath = false;                       //죽음 체크

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

        //힐
        public void Heal(float amount)
        {
            float beforeHealth = CurrentHealth;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

            //real Heal 구하기
            float realHeal = CurrentHealth - beforeHealth;
            if (realHeal > 0f)
            {
                //힐 구현
                OnHeal?.Invoke(realHeal);
            }
        }

        //damageSource: 데미지를 주는 주체
        public void TakeDamage(float damage)
        {
            // 방어력 적용 후 최종 데미지 계산
            float mitigatedDamage = Mathf.Max(damage - CurrentArmor, 0); // Clamp 대신 Max 사용 (더 간결)

            // 실질적으로 들어온 데미지 계산
            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

            if (realDamage > 0f)
            {
                // 체력 감소, 데미지이펙트 구현
                CurrentHealth -= realDamage;          
                OnDamaged?.Invoke(realDamage);
            }

            // 체력이 0 이하라면 죽음 처리
            if (CurrentHealth <= 0f)
            {
                HandleDeath();
            }
        }

        //죽음 처리 관리
        void HandleDeath()
        {
            //죽음 체크
            if (isDeath)
                return;

            if (CurrentHealth <= 0f)
            {
                isDeath = true;

                //죽음 구현
                OnDie?.Invoke();
            }
        }
    }
}