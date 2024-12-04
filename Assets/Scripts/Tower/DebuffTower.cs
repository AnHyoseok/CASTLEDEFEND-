using Defend.Projectile;
using Defend.TestScript;
using Defend.Utillity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 슬로우타워 기능 정의
/// 범위 내 적 유닛에게 디버프를 부여하여 이동속도 저하
/// </summary>
namespace Defend.Tower
{
    // TODO :: 플레이어가 활성화 여부를 결정하는 기능
    // => off시 모든 기능 정지, on시 모든 기능 활성화
    public class DebuffTower : MultipleTower
    {
        private HashSet<Transform> attackedTargets = new HashSet<Transform>(); // 이미 공격한 타겟 저장
        [SerializeField] private float manaAmount;
        [SerializeField] private bool isOn => status.CurrentMana >= manaAmount;
        [SerializeField] private GameObject effectObj;
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            ActivatedEffect();
            InvokeRepeating(nameof(CleanUpAttackedTargets), 0f, 3f);
        }
        protected override void Shoot()
        {
            // 슛 딜레이와 타겟 유효성 검사
            if (towerInfo.shootDelay >= shootTime) return;

            // 유효한 타겟들 가져오기
            var allTargets = UpdateTargets();

            // 유효한 타겟이 없으면 종료
            if (allTargets.Count == 0) return;

            // 공격하지 않은 타겟만 대상으로 발사체 생성
            foreach (var target in allTargets)
            {
                // 이미 적용한 적인지, 소모 마나보다 많이 보유한 경우
                if (!attackedTargets.Contains(target) && status.CurrentMana >= manaAmount)
                {
                    // 마나 소모
                    status.UseMana(manaAmount);

                    // 발사체 인스턴스 생성
                    GameObject projectilePrefab = Instantiate(
                        towerInfo.projectile.prefab,
                        firePoint.transform.position,
                        Quaternion.identity
                    );

                    // 발사체 초기화
                    projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, target);

                    // 타겟을 공격된 목록에 추가
                    attackedTargets.Add(target);

                    // 애니메이션 재생
                    if (animator != null)
                        animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);
                }
            }

            // 슛 타임 초기화
            shootTime = 0;
        }

        // 활성화 상태의 Effect On/Off
        private void ActivatedEffect()
        {
            effectObj.SetActive(isOn);
        }

        // null 또는 Missing 상태의 타겟 제거
        private void CleanUpAttackedTargets()
        {
            attackedTargets.RemoveWhere(target => target == null);
        }
    }
}