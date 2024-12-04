using Defend.Projectile;
using Defend.TestScript;
using Defend.Utillity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 멀티타워 기능 정의
/// 정해진 여러 타겟들을 확정적으로 한발 공격
/// </summary>
namespace Defend.Tower
{
    public class MultipleTower : TowerBase
    {
        [SerializeField] protected int maxCount;            // 최대 발사체 생성 갯수
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        List<Transform> GetClosestTargets()
        {
            // 타겟 목록을 거리 기준으로 정렬
            var sortedTargets = UpdateTargets()
                .OrderBy(target => Vector3.Distance(transform.position, target.position))          // 거리 기준 정렬
                .Take(maxCount)                                                                    // 최대 maxCount만큼 선택
                .ToList();

            return sortedTargets;
        }

        // Shoot 재정의
        protected override void Shoot()
        {
            // 슛 딜레이와 타겟 유효성 검사
            if (towerInfo.shootDelay >= shootTime) return;

            // 가장 가까운 타겟 3개 가져오기
            var closestTargets = GetClosestTargets();

            // 유효한 타겟이 없으면 종료
            if (closestTargets.Count == 0) return;

            // 발사체 생성
            foreach (var target in closestTargets)
            {
                // 발사체 인스턴스 생성
                GameObject projectilePrefab = Instantiate(
                    towerInfo.projectile.prefab,
                    firePoint.transform.position,
                    Quaternion.identity
                );

                // 발사체 초기화
                projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, target);

                // 애니메이션 재생
                if (animator != null)
                    animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);
            }

            // 슛 타임 초기화
            shootTime = 0;
        }
    }
}
