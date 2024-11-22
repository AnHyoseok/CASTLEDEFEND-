using UnityEngine;

/// <summary>
/// 버프타워 기능 정의
/// 주변 아군 타워의 능력치를 항샹 시키는 타워
/// </summary>
namespace Defend.Tower
{
    public class BuffTower : TowerBase
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