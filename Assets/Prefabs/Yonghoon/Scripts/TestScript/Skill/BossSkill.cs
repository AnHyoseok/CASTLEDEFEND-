using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;

namespace Defend.Enemy.Skill
{
    public class BossSkill : SkillBase
    {
        #region Variables
        private float speedAmount = 1f;
        private float healthRatio;
        private List<float> thresholds = new List<float> { 1f, 0.75f, 0.50f }; // 체력 비율 기준
        private HashSet<float> usedThresholds = new HashSet<float>(); // 이미 사용된 체력 구간 추적
        private Dictionary<float, SkillType> skillMapping; // 체력 구간과 스킬 타입 매핑
        #endregion

        private enum SkillType
        {
            IncreaseSpeed,
            IncreaseArmor,
            IncreaseDamage
        }

        private void Start()
        {
            // 체력 구간에 따른 스킬 매핑
            skillMapping = new Dictionary<float, SkillType>
            {
                { 1f, SkillType.IncreaseSpeed },
                { 0.75f, SkillType.IncreaseArmor },
                { 0.5f, SkillType.IncreaseDamage }
            };
        }

        public override void ActivateSkill()
        {
            Debug.Log("Boss uses a skill!");

            foreach (var threshold in thresholds)
            {
                if (healthRatio <= threshold && !usedThresholds.Contains(threshold))
                {
                    Debug.Log("currentHealthRatio");
                    usedThresholds.Add(threshold); // 사용된 구간 추가
                    ExecuteSkill(skillMapping[threshold]); // 해당 스킬 실행
                    break;
                }
            }
        }

        private void ExecuteSkill(SkillType skillType)
        {
            Debug.Log("ExecuteSkill");
            switch (skillType)
            {
                case SkillType.IncreaseArmor:
                    IncreaseArmor();
                    break;

                case SkillType.IncreaseDamage:
                    IncreaseDamage();
                    break;

                case SkillType.IncreaseSpeed:
                    IncreaseSpeed();
                    break;
            }
        }

        private void IncreaseArmor()
        {
            Debug.Log("Buffing all allies!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<Health>();
                if (enemyController != null)
                {
                    enemyController.ChangedArmor(amount);
                    Debug.Log($"{enemyController.gameObject.name}의 버프 파워가 증가했습니다!");
                }
            }
        }

        private void IncreaseDamage()
        {
            Debug.Log("Increasing damage for all allies!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyAttackController>();
                if (enemyController != null)
                {
                    enemyController.ChangedAttackDamage(amount);
                    Debug.Log($"{enemyController.gameObject.name}의 공격력이 증가했습니다!");
                }
            }
        }

        private void IncreaseSpeed()
        {
            Debug.Log("Applying area damage!");

            int layerMask = LayerMask.GetMask("Enemy", "Boss");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, layerMask);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyMoveController>();
                if (enemyController != null)
                {
                    enemyController.ChangedMoveSpeed(this.gameObject, speedAmount);
                }
            }
        }

        public override bool CanActivateSkill(float healthRatio)
        {
            this.healthRatio= healthRatio;
            foreach (var threshold in thresholds)
            {
                // 특정 체력 구간에 도달했으며, 아직 사용되지 않은 경우 발동 가능
                if (healthRatio <= threshold && !usedThresholds.Contains(threshold))
                {
                    return true;
                }
            }

            return false;
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
