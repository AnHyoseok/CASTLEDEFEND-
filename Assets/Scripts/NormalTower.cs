using UnityEngine;

/// <summary>
/// �⺻Ÿ���� ����
/// ������ Ÿ���� Ȯ�������� �ѹ� ����
/// </summary>
public class NormalTower : TowerBase
{
    protected override void Start()
    {
        base.Start();
        attackRange = 5f;
    }

    protected override void Update()
    {
        base.Update();
    }
}
