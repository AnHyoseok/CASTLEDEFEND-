using Defend.Utillity;
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
            status = GetComponent<Status>();

            status.Init(towerInfo);
        }

        protected override void Update()
        {

        }
    }
}