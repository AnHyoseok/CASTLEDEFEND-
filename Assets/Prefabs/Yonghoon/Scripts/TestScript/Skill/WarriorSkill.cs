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
        #region Variables
        [SerializeField] private float increaseAttackPowerAmount = 5f;
        [SerializeField] private float skillDuration = 5f;  // 공격력 증가 지속 시간
        [SerializeField] private bool hasSkill = false;
        #endregion

        #region Test용 Range
        [SerializeField] private float range = 5f;
        #endregion
         
        public override void ActivateSkill()
        {
            hasSkill = true;

            Debug.Log("전사가 공격력 증가 버프 사용!");

            // 범위 내 적 탐색
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range);

            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(increaseAttackPowerAmount); // 공격력 증가
                    Debug.Log($"{enemyStats.gameObject.name}의 공격력이 {increaseAttackPowerAmount}만큼 증가했습니다!");

                    // 지속 시간 동안 공격력 증가 후 원래 값으로 복귀
                    enemyStats.StartCoroutine(RestoreAttackAfterDuration(enemyStats, skillDuration));
                }
            }
            //ShowEffect(activator, range);
        }

        //추후 이펙트를 추가하게되면 설정
        //private void ShowEffect(Transform activator, float range)
        //{
        //    Debug.Log($"Warrior 포효 효과 발생! 범위: {range}");
        //}

        // 공격력 증가 후 지속 시간 동안만 유지되게 하는 코루틴
        private IEnumerator RestoreAttackAfterDuration(EnemyAttackController enemyStats, float duration)
        {
            yield return new WaitForSeconds(duration);

            // 공격력을 원래 값으로 복구
            enemyStats.ChangedAttackDamage(-increaseAttackPowerAmount);
            Debug.Log($"{enemyStats.gameObject.name}의 방어력이 {increaseAttackPowerAmount}만큼 다시 감소했습니다!");
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