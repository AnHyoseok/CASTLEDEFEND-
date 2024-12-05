using Defend.TestScript;
/// <summary>
/// DebuffArmor 발사체 기능 구현
/// 원거리, 다중, 방어력 저하
/// </summary>
namespace Defend.Projectile
{
    public class DebuffArmor : Debuffer<Health>
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
        protected override void DoDebuffAction(Health component)
        {
            component.ChangedArmor(projectileInfo.debuffRatio); // 방어력 감소
        }

        // 디버프 제거 발동
        protected override void UndoDebuffAction(Health component)
        {
            component.ChangedArmor(-projectileInfo.debuffRatio); // 방어력 원상복구
        }
    }
}
