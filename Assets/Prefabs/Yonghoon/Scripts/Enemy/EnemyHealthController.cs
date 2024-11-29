//using UnityEngine;

//namespace Defend.Enemy
//{
//    public class EnemyHealthController : MonoBehaviour
//    {
//        #region Variables
//        EnemyState enemyBase;
//        [SerializeField] public float CurrentHealth { get; private set; }
//        [SerializeField] public float CurrentArmor { get; private set; }
//        #endregion

//        private void Start()
//        {
//            enemyBase = GetComponent<EnemyState>();
//            CurrentHealth = enemyBase.baseHealth;
//            CurrentArmor = enemyBase.baseArmor;
//        }

//        public void TakeDamage(float damage)
//        {
//            // 방어력 적용 후 최종 데미지 계산
//            float mitigatedDamage = Mathf.Clamp(damage - CurrentArmor, 0, Mathf.Infinity);

//            // 실질적으로 들어온 데미지 계산
//            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

//            // 체력 감소
//            CurrentHealth -= realDamage;

//            // 로그나 디버깅 용도로 실질적 데미지를 출력 (옵션)
//            Debug.Log($"Damage Taken: {realDamage}, Remaining Health: {CurrentHealth}");

//            // 체력이 0 이하라면 사망 처리
//            if (CurrentHealth <= 0f)
//            {
//                //데미지가 체력보다 높았을경우, 데미지 수치가 필요할시 사용
//                //float overkillDamage = Mathf.Max(0, mitigatedDamage - CurrentHealth);
//                //Debug.Log($"Overkill Damage: {overkillDamage}");

//                Die();
//            }
//        }

//        //힐
//        public void Heal(float amount)
//        {
//            // 힐 적용 전 체력 저장
//            float beforeHealth = CurrentHealth;

//            // 힐 적용
//            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, enemyBase.baseHealth);

//            // 실제 힐량 계산
//            float realHeal = CurrentHealth - beforeHealth;

//            //**************************************************
//            // 오버힐량 계산 => ex) 만약 힐량이 최대체력보다 높아지면 쉴드값을 준다거나, 추가적인 버프를 준다거나 필요시 사용! 
//            //float overheal = Mathf.Max(0f, amount - realHeal);
//            //**************************************************

//            // 디버깅 또는 로그 출력
//            Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
//        }


//        public void ReduceArmor(float amount)
//        {
//            CurrentArmor -= amount;
//        }

//        public void Die()
//        {
//            Debug.Log("Enemy Die!");
//            Destroy(gameObject);
//        }
//    }
//}