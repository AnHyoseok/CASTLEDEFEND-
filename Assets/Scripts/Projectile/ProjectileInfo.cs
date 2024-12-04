using UnityEngine;
/// <summary>
/// 발사체의 정보를 정의
/// </summary>
namespace Defend.Projectile
{
    [System.Serializable]
    public class ProjectileInfo
    {
        public GameObject tower;        // 발사체의 타워
        public GameObject prefab;       // 발사체 프리팹
        public GameObject effectPrefab; // 이펙트 프리팹
        public float effectTime;        // 이펙트 지속시간
        public float attack;            // 공격력
        public float moveSpeed;         // 이동속도
        public float attackRange;       // 타격 범위
        public float debuffRatio;       // 디버프 비율
    }
}
