using Defend.TestScript;
using UnityEngine;

namespace Defend.Enemy.Skill
{
    public class WizardSkill : SkillBase
    {
        #region Variables
        private float lastSkillTime = -Mathf.Infinity; // 마지막 스킬 발동 시간
        #endregion

        public override void ActivateSkill()
        {
            //Debug.Log("Wizard uses Heal skill!");

            float healAmount = gameObject.GetComponent<EnemyAttackController>().CurrentAttackDamage;

            //Debug.Log(healAmount);

            // 범위 내 특정 레이어의 Collider 검색
            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);

            foreach (var collider in hitColliders)
            {
                // GetComponentInParent로 EnemyStats 가져오기
                var enemyStats = collider.GetComponentInParent<Health>();
                if (enemyStats != null)
                {

                    enemyStats.Heal(healAmount); // 힐
                    //Debug.Log($"{enemyStats.gameObject.name}의 체력이 {healAmount}만큼 증가했습니다!");
                }
            }
            lastSkillTime = Time.time;
        }

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