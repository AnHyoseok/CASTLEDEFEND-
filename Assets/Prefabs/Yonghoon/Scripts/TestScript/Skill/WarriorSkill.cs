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
        #endregion

        #region Test용 Range
        private float range = 5f;
        #endregion

        public override void ActivateSkill(Transform activator, float range)
        {
            Debug.Log("Warrior uses Roar skill!");

            // 범위 내 적 탐색
            Collider[] hitEnemies = Physics.OverlapSphere(activator.position, range);

            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStats = enemyCollider.GetComponentInParent<EnemyAttackController>();
                if (enemyStats != null)
                {
                    enemyStats.ChangedAttackDamage(increaseAttackPowerAmount); // 공격력 증가
                    Debug.Log($"{enemyStats.gameObject.name}의 공격력이 증가했습니다!");
                }
            }
            //ShowEffect(activator, range);
        }

        //추후 이펙트를 추가하게되면 설정
        private void ShowEffect(Transform activator, float range)
        {
            Debug.Log($"Warrior 포효 효과 발생! 범위: {range}");
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