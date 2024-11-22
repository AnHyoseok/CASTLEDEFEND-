using UnityEngine;

/// <summary>
/// 슬로우타워 기능 정의
/// 범위 내 적 유닛의 이동속도 저하
/// </summary>
namespace Defend.Tower
{
    public class SlowTower : TowerBase
    {
        protected override void Start()
        {
            base.Start();
            towerInfo.attackRange = 7f;
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}