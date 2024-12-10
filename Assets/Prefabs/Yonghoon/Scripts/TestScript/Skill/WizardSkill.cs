using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class WizardSkill : SkillBase
    {

        [SerializeField] private float skillCooldown = 5f; // 스킬 발동 주기
        private float lastSkillTime = -Mathf.Infinity; // 마지막 스킬 발동 시간

        #region Test용 Range
        [SerializeField] private float range = 5f;
        #endregion

        public override void ActivateSkill()
        {
            Debug.Log("Wizard uses Heal skill!");

            float healAmount = gameObject.GetComponent<EnemyAttackController>().CurrentAttackDamage;

            Debug.Log(healAmount);

            // 범위 내 모든 Collider 검색
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent로 EnemyStats 가져오기
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {

                    enemyStats.Heal(healAmount); // 방어력 증가
                    Debug.Log($"{enemyStats.gameObject.name}의 체력이 {healAmount}만큼 증가했습니다!");
                }
            }

            // 선택적 이펙트
            //ShowEffect(activator, range);
        }

        //private void ShowEffect(Transform activator, float range)
        //{
        //    Debug.Log($"Heal 효과 발생! 범위: {range}");
        //}

        public override bool CanActivateSkill(float healthRatio)
        {
            return Time.time - lastSkillTime >= skillCooldown;
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