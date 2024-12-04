using Defend.Enemy;
using Defend.TestScript;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 지점을 공격하는 발사체의 기능을 정의
/// </summary>
namespace Defend.Projectile
{
    // TODO :: Rocket의 경우 지정된 지점에 적과 무관하게 계속 공격하도록 하는 기능?
    public class PointProjectile : ProjectileBase
    {
        [SerializeField] protected Vector3 pointTarget;             // 목표 지점

        #region collider 활용 => 미사용
        //[SerializeField] protected Collider[] colliders;          // collider 오브젝트들
        #endregion

        protected override void Start()
        {

        }

        protected override void Update()
        {
            MoveToPoint();
            if (ArrivalPoint() == true) { Hit(); }
        }

        // Init 메서드를 오버라이드하여 target이 설정될 때 해당 지점을 기록
        public override void Init(ProjectileInfo _projectileInfo, Transform closestTarget)
        {
            base.Init(_projectileInfo, closestTarget);
            // target이 설정되면 그 위치를 기록
            pointTarget = closestTarget.position;
        }

        // 지점을 향해 이동
        protected virtual void MoveToPoint()
        {
            // 지점을 바라보도록 
            transform.LookAt(pointTarget);
            // 지점을 향해 이동
            transform.position = Vector3.MoveTowards(transform.position, pointTarget, Time.deltaTime * projectileInfo.moveSpeed);
        }

        // 지점에 도착 여부
        protected virtual bool ArrivalPoint()
        {
            // 타겟까지의 벡터
            Vector3 dir = pointTarget - transform.position;
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
    }
}