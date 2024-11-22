using UnityEngine;

/// <summary>
/// 디버프타워 기능 정의
/// 범위 내 적의 방어력을 깎는 타워
/// </summary>
namespace Defend.Tower
{
    public class DebuffTower : TowerBase
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