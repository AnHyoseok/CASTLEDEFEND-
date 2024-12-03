using Defend.TestScript;
using UnityEngine;
/// <summary>
/// 캐논 발사체 정의
/// 단거리, 범위, 폭발
/// </summary>
namespace Defend.Projectile
{
    public class Cannon : TargetProjectile
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
            HitOnRange();
        }
    }
}