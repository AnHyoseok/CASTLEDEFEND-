using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class TankerSkill : SkillBase
    {
        #region Variables
        [SerializeField] private float increaseArmorAmount = 5f;
        [SerializeField] private float skillDuration = 5f;  // 방어력 증가 지속 시간
        [SerializeField] private bool hasSkill = false;
        #endregion

        #region Test용 Range
        [SerializeField] private float range = 5f;
        #endregion

        public override void ActivateSkill()
        {
            hasSkill = true;
            
            Debug.Log("탱커가 방어력 증가 버프 사용");

            // 범위 내 모든 Collider 검색
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent로 EnemyStats 가져오기
                var healthComponent = collider.GetComponentInParent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.ChangedArmor(increaseArmorAmount); // 방어력 증가
                    Debug.Log($"{healthComponent.gameObject.name}의 방어력이 {increaseArmorAmount}만큼 증가했습니다!");

                    // 지속 시간 동안 방어력 증가 후 원래 값으로 복귀
                    healthComponent.StartCoroutine(RestoreArmorAfterDuration(healthComponent, skillDuration));
                }
            }

            // 선택적 이펙트
            //ShowEffect(activator, range);
        }

        private void ShowEffect(Transform activator, float range)
        {
            Debug.Log($"Warrior 포효 효과 발생! 범위: {range}");
        }

        // 방어력 증가 후 지속 시간 동안만 유지되게 하는 코루틴
        private IEnumerator RestoreArmorAfterDuration(Health healthComponent, float duration)
        {
            yield return new WaitForSeconds(duration);

            // 방어력을 원래 값으로 복구
            healthComponent.ChangedArmor(-increaseArmorAmount);
            Debug.Log($"{healthComponent.gameObject.name}의 방어력이 {increaseArmorAmount}만큼 다시 감소했습니다!");
        }

        public override bool CanActivateSkill(float healthRatio)
        {
            // 체력이 50% 이하일 때 발동
            return healthRatio <= 0.5f && !hasSkill;
        }

        #region Test용 GIZMO
        private void OnDrawGizmosSelected()
        {
            // Gizmo로 포효 범위를 시각적으로 표시
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        #endregion

    }
}