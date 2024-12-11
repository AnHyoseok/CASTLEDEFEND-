using Defend.TestScript;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    /// <summary>
    /// Enemy(Warrior) Skill을 정의하는 클래스
    /// </summary>
    public class WarriorSkill : SkillBase
    {
        public override void ActivateSkill()
        {
            hasSkill = true;

            //Debug.Log("전사가 공격력 증가 버프 사용!");

            // 범위 내 특정 레이어의 Collider 검색
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);

            foreach (var enemyCollider in hitColliders)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(amount); // 공격력 증가
                    //Debug.Log($"{enemyStats.gameObject.name}의 공격력이 {amount}만큼 증가했습니다!");

                    // 지속 시간 동안 공격력 증가 후 원래 값으로 복귀
                    enemyStats.StartCoroutine(RestoreAttackAfterDuration(enemyStats, skillDuration));
                }
            }
        }

        // 공격력 증가 후 지속 시간 동안만 유지되게 하는 코루틴
        private IEnumerator RestoreAttackAfterDuration(EnemyAttackController enemyStats, float duration)
        {
            yield return new WaitForSeconds(duration);

            // 공격력을 원래 값으로 복구
            enemyStats.ChangedAttackDamage(-amount);
            //Debug.Log($"{enemyStats.gameObject.name}의 방어력이 {amount}만큼 다시 감소했습니다!");
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