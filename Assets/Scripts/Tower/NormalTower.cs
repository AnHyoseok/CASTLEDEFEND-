using UnityEngine;

/// <summary>
/// �⺻Ÿ�� ��� ����
/// ������ Ÿ���� Ȯ�������� �ѹ� ����
/// </summary>
namespace Defend.Tower
{
    public class NormalTower : TowerBase
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