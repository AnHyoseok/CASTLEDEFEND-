using UnityEngine;

/// <summary>
/// 멀티타워 기능 정의
/// 정해진 여러 타겟들을 확정적으로 한발 공격
/// </summary>
namespace Defend.Tower
{
    public class MultipleTower : TowerBase
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
