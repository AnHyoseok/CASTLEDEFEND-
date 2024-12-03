using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Bat 발사체 정의
/// 단거리, 단일, 표창
/// </summary>
namespace Defend.Projectile
{
    public class Bat : TargetProjectile
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