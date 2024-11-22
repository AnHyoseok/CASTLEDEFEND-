using UnityEngine;

/// <summary>
/// 기본타워를 정의
/// 정해진 타겟을 확정적으로 한발 공격
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
