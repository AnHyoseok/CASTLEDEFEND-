using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Ballista 발사체 정의
/// 원거리, 단일, 화살
/// </summary>
namespace Defend.Projectile
{
    public class Ballista : TargetProjectile
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void Hit()
        {
            base.Hit();
            HitOnTarget();
        }
    }
}