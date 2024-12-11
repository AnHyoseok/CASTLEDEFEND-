using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;

namespace Defend.Enemy.Skill
{
    public class BossSkill : SkillBase
    {
        #region Variables

        private List<float> thresholds = new List<float> { 0.75f, 0.5f, 0.25f }; // ü�� ���� ����
        private HashSet<float> usedThresholds = new HashSet<float>(); // �̹� ���� ü�� ���� ����
        private Dictionary<float, SkillType> skillMapping; // ü�� ������ ��ų Ÿ�� ����

        [SerializeField] private float range = 20f; // ����
        [SerializeField] private float damageIncrease = 50f; // ���ݷ� ������
        [SerializeField] private float buffPowerIncrease = 30f; // ���� �Ŀ� ������
        [SerializeField] private float aoeDamage = 100f; // ���� ������
        #endregion

        private enum SkillType
        {
            BuffAllies,
            IncreaseDamage,
            AreaOfEffect
        }

        private void Start()
        {
            // ü�� ������ ���� ��ų ����
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
                    usedThresholds.Add(threshold); // ���� ���� �߰�
                    ExecuteSkill(skillMapping[threshold]); // �ش� ��ų ����
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
                    Debug.Log($"{enemyController.gameObject.name}�� ���� �Ŀ��� �����߽��ϴ�!");
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
                    Debug.Log($"{enemyController.gameObject.name}�� ���ݷ��� �����߽��ϴ�!");
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
                    Debug.Log($"{health.gameObject.name}���� {aoeDamage}�� ���ظ� �������ϴ�!");
                }
            }
        }

        public override bool CanActivateSkill(float healthRatio)
        {
            foreach (var threshold in thresholds)
            {
                // Ư�� ü�� ������ ����������, ���� ������ ���� ��� �ߵ� ����
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
