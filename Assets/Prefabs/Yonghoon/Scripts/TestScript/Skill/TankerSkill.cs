using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class TankerSkill : SkillBase
    {
        public override void ActivateSkill()
        {
            hasSkill = true;

            //Debug.Log("탱커가 방어력 증가 버프 사용");

            // 범위 내 특정 레이어의 Collider 검색
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);


            foreach (var collider in hitColliders)
            {
                // GetComponentInParent로 EnemyStats 가져오기
                var healthComponent = collider.GetComponentInParent<Health>();
                if (healthComponent != null)
                {
                    healthComponent.ChangedArmor(amount); // 방어력 증가
                    //Debug.Log($"{healthComponent.gameObject.name}의 방어력이 {amount}만큼 증가했습니다!");

                    // 지속 시간 동안 방어력 증가 후 원래 값으로 복귀
                    healthComponent.StartCoroutine(RestoreArmorAfterDuration(healthComponent, skillDuration));
                }
            }
        }

        // 방어력 증가 후 지속 시간 동안만 유지되게 하는 코루틴
        private IEnumerator RestoreArmorAfterDuration(Health healthComponent, float duration)
        {
            yield return new WaitForSeconds(duration);

            // 방어력을 원래 값으로 복구
            healthComponent.ChangedArmor(-amount);
            //Debug.Log($"{healthComponent.gameObject.name}의 방어력이 {amount}만큼 다시 감소했습니다!");
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