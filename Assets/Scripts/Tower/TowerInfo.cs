using Defend.Projectile;
using UnityEngine;

/// <summary>
/// 타워의 정보를 정의
/// </summary>
namespace Defend.Tower
{
    [System.Serializable]
    public class TowerInfo
    {
        public float rotationSpeed = 5f;    // 회전 속도
        public float attackRange = 5f;      // 공격 사거리
        public float detectDelay = 0.5f;    // 타겟 설정 딜레이
        public float shootDelay = 1.0f;     // 슛 딜레이
        public float maxHealth = 200f;      // 최대 체력
        public float maxMana = 50f;         // 최대 마나
        public float armor = 5f;            // 방어력
        public float healthRegen = 1f;      // 체력 재생력
        public float manaRegen = 1f;        // 마나 재생력
        public float cost1 = 1f;            // 건설비용 1
        public float cost2 = 2f;            // 건설비용 2
        public float cost3 = 3f;            // 건설비용 3
        public float cost4 = 4f;            // 건설비용 4
        public GameObject upgradeTower;     // 업그레이드 타워 프리팹
        public ProjectileInfo projectile;   // 발사체 정보
        public bool isLock = false;         // 해금 여부

        //판매 가격
        public float GetSellCost()
        {
            return cost1 / 2;
        }

        //나무 가격
        public float GetSellTreeCost()
        {
            return cost3 / 2;
        }
        //돌 가격
        public float GetSellRockCost()
        {
            return cost4 / 2;
        }
    }
}
