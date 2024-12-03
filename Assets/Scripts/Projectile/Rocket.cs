using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Rocket 발사체 정의
/// 단거리, 지점, 폭발
/// </summary>
namespace Defend.Projectile
{
    public class Rocket : PointProjectile
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