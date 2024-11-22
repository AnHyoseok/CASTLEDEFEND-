using UnityEngine;

/// <summary>
/// 레이저타워 기능 정의
/// 정해진 타겟을 확정적으로 레이저 공격
/// </summary>
namespace Defend.Tower
{
    public class LaserTower : TowerBase
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