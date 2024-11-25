using UnityEngine;
/// <summary>
/// 발사체의 정보를 정의
/// </summary>
namespace Defend.Projectile
{
    [System.Serializable]
    public class ProjectileInfo
    {
        public GameObject prefab;       // 발사체 프리팹
        public float attack;            // 공격력
        public float moveSpeed;         // 이동속도
    }
}
