using Defend.Utillity;
using UnityEngine;

/// <summary>
/// ���ο�Ÿ�� ��� ����
/// ���� �� �� ������ �̵��ӵ� ����
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