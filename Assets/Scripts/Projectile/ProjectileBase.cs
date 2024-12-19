using Defend.TestScript;
using UnityEngine;
using static Defend.Utillity.AudioUtility;
/// <summary>
/// �߻�ü�� ����� ������ ���� Ŭ����
/// </summary>
namespace Defend.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Transform target;                // ��ǥ��
        [SerializeField] protected Vector3 targetPosition;          // ��ǥ��
        [SerializeField] protected ProjectileInfo projectileInfo;   // �߻�ü ����
        [SerializeField] protected Vector3 offsetPosition;          // Position ����
        [SerializeField] protected float offsetScale;               // Scale ����

        // TODO :: Bat, Rocket sfx clip ã�Ƽ� �߰��ϱ�
        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {

        }

        // Ÿ�ٰ� �߻�ü ���� �ʱ�ȭ
        public virtual void Init(ProjectileInfo _projectileInfo, Transform closestTarget)
        {
            target = closestTarget;
            offsetPosition = target.gameObject.GetComponent<EnemyController>().positionOffset;
            offsetScale = target.gameObject.GetComponent<EnemyController>().scaleOffset;
            targetPosition = target.position + offsetPosition;
            transform.GetChild(0).localScale *= offsetScale;
            projectileInfo = _projectileInfo;
        }

        // Ÿ�ٿ� Projectile Effect & Sound ����
        protected virtual void Hit()
        {
            // Projectile Effect ����
            if (projectileInfo.effectPrefab != null)
            {
                GameObject effect = Instantiate(projectileInfo.effectPrefab, transform.position, Quaternion.identity);
                effect.transform.localScale *= offsetScale;
                // Projectile Effect ���� ����
                Destroy(effect, projectileInfo.effectTime);
            }
            // Projectile Sound ����
            if(projectileInfo.sfxClip != null)
            {
                CreateSFX(projectileInfo.sfxClip, transform.position, AudioGroups.EFFECT);
            }
            // Projectile ���� 
            Destroy(this.gameObject);
        }

        // Ÿ���� ���� ���� ��ŭ ��������
        protected void HitOnRange()
        {
            // EnemyController ������Ʈ�� ���� Object ã��
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (var obj in enemies)
            {
                // �Ÿ� üũ
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance <= projectileInfo.attackRange)
                {
                    Health health = obj.GetComponent<Health>();
                    if (health != null)
                    {
                        // ������ �ֱ�
                        health.TakeDamage(projectileInfo.attack);
                    }
                }
            }
        }

        // Ÿ�ٸ� ����
        protected void HitOnTarget()
        {
            // Health ������Ʈ ����
            Health health = target.GetComponent<Health>();
            // ������ �ֱ�
            health.TakeDamage(projectileInfo.attack);
        }


        // ���ݹ��� �����
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, projectileInfo.attackRange);
        }
    }
}