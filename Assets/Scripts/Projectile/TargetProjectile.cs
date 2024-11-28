using Defend.Enemy;
using Defend.TestScript;
using UnityEngine;
/// <summary>
/// 타겟을 공격하는 발사체의 기능을 정의
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

        // 타겟을 향해 이동
        protected virtual void MoveToTarget()
        {
            // 타겟을 바라보도록 
            transform.LookAt(target);
            // 타겟을 향해 이동
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // 타겟에 도착 여부
        protected virtual bool ArrivalTarget()
        {
            // 타겟까지의 벡터
            Vector3 dir = target.position - transform.position;
            // 발사체가 한프레임당 이동하는 거리
            float distanceThisFrame = Time.deltaTime * projectileInfo.moveSpeed;
            if (dir.magnitude < distanceThisFrame)
            {
                // 도착
                return true;
            }
            // 미도착
            return false;
        }
        // 타격
        protected override void Hit()
        {
            base.Hit();
            // Health 컴포넌트 접근
            Health ehc = target.GetComponent<Health>();
            // 데미지 주기
            ehc.TakeDamage(projectileInfo.attack);
        }
    }
}