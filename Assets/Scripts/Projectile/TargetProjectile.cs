using Defend.Enemy;
using UnityEngine;
/// <summary>
/// Ÿ���� �����ϴ� �߻�ü�� ����� ����
/// </summary>
namespace Defend.Projectile
{
    public class TargetProjectile : ProjectileBase
    {
        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }

            MoveToTarget();
            if (ArrivalTarget() == true) { Hit(); }
        }

        // Ÿ���� ���� �̵�
        protected virtual void MoveToTarget()
        {
            // Ÿ���� �ٶ󺸵��� 
            transform.LookAt(target);
            // Ÿ���� ���� �̵�
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // Ÿ�ٿ� ���� ����
        protected virtual bool ArrivalTarget()
        {
            // Ÿ�ٱ����� ����
            Vector3 dir = target.position - transform.position;
            // �߻�ü�� �������Ӵ� �̵��ϴ� �Ÿ�
            float distanceThisFrame = Time.deltaTime * projectileInfo.moveSpeed;
            if (dir.magnitude < distanceThisFrame)
            {
                // ����
                return true;
            }
            // �̵���
            return false;
        }
        // Ÿ��
        protected override void Hit()
        {
            base.Hit();
            // EnemyHealthController ������Ʈ ����
            EnemyHealthController ehc = target.GetComponent<EnemyHealthController>();
            // ������ �ֱ�
            ehc.TakeDamage(projectileInfo.attack);
        }
    }
}