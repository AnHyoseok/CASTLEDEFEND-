using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;

namespace Defend.Enemy.Skill
{
    public class BossSkill : SkillBase
    {
        #region Variables

        private List<float> thresholds = new List<float> { 0.75f, 0.5f, 0.25f }; // 체력 비율 기준
        private HashSet<float> usedThresholds = new HashSet<float>(); // 이미 사용된 체력 구간 추적
        private Dictionary<float, SkillType> skillMapping; // 체력 구간과 스킬 타입 매핑

        [SerializeField] private float range = 20f; // 범위
        [SerializeField] private float damageIncrease = 50f; // 공격력 증가량
        [SerializeField] private float buffPowerIncrease = 30f; // 버프 파워 증가량
        [SerializeField] private float aoeDamage = 100f; // 범위 데미지
        #endregion

        private enum SkillType
        {
            BuffAllies,
            IncreaseDamage,
            AreaOfEffect
        }

        private void Start()
        {
            // 체력 구간에 따른 스킬 매핑
            skillMapping = new Dictionary<float, SkillType>
            {
                { 0.75f, SkillType.BuffAllies },
                { 0.5f, SkillType.IncreaseDamage },
                { 0.25f, SkillType.AreaOfEffect }
            };
        }

        public override void ActivateSkill()
        {
            Debug.Log("Boss uses a skill!");

            float currentHealthRatio = GetCurrentHealthRatio();

            foreach (var threshold in thresholds)
            {
                if (currentHealthRatio <= threshold && !usedThresholds.Contains(threshold))
                {
                    usedThresholds.Add(threshold); // 사용된 구간 추가
                    ExecuteSkill(skillMapping[threshold]); // 해당 스킬 실행
                    break;
                }
            }
        }

        private void ExecuteSkill(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.BuffAllies:
                    BuffAllies();
                    break;

                case SkillType.IncreaseDamage:
                    IncreaseDamage();
                    break;

                case SkillType.AreaOfEffect:
                    AreaOfEffect();
                    break;
            }
        }

        private void BuffAllies()
        {
            Debug.Log("Buffing all allies!");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyAttackController>();
                if (enemyController != null)
                {
                    enemyController.ChangedAttackDamage(buffPowerIncrease);
                    Debug.Log($"{enemyController.gameObject.name}의 버프 파워가 증가했습니다!");
                }
            }
        }

        private void IncreaseDamage()
        {
            Debug.Log("Increasing damage for all allies!");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in hitColliders)
            {
                var enemyController = collider.GetComponentInParent<EnemyAttackController>();
                if (enemyController != null)
                {
                    enemyController.ChangedAttackDamage(damageIncrease);
                    Debug.Log($"{enemyController.gameObject.name}의 공격력이 증가했습니다!");
                }
            }
        }

        private void AreaOfEffect()
        {
            Debug.Log("Applying area damage!");

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in hitColliders)
            {
                var health = collider.GetComponentInParent<Health>();
                if (health != null)
                {
                    health.TakeDamage(aoeDamage);
                    Debug.Log($"{health.gameObject.name}에게 {aoeDamage}의 피해를 입혔습니다!");
                }
            }
        }

        public override bool CanActivateSkill(float healthRatio)
        {
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

        private float GetCurrentHealthRatio()
        {
            var healthComponent = GetComponent<Health>();
            //return healthComponent != null ? healthComponent.CurrentHealth / healthComponent.MaxHealth : 1f;
            return 1f;
        }
    }
}
