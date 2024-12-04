using Defend.Enemy;
/// <summary>
/// Slower 발사체 기능 구현
/// 원거리, 다중, 이동속도 저하
/// </summary>
namespace Defend.Projectile
{
    public class Slower : Debuffer<EnemyMoveController>
    {
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        // 디버프 액션 발동
        protected override void DoDebuffAction(EnemyMoveController component)
        {
            component.ChangedMoveSpeed(this.gameObject,projectileInfo.debuffRatio); // 이동 속도 감소
        }

        // 디버프 제거 발동
        protected override void UndoDebuffAction(EnemyMoveController component)
        {
            component.RemoveMoveSource(this.gameObject); // 이동 속도 원상복구
        }
    }
}