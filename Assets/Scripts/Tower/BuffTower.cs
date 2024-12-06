using Defend.Utillity;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버프타워 기능 정의
/// 주변 아군 타워의 능력치를 항샹 시키는 타워
/// </summary>
namespace Defend.Tower
{
    // Buff 할 요소들
    [System.Serializable]
    public class BuffContents
    {
        public float duration;              // 지속시간
        public float atk;                   // 공격력 증가
        public float armor;                 // 방어력 증가
        public float shootDelay;            // 연사력 증가
        public float atkRange;              // 공격범위 증가
        public float healthRegen;           // 체력 재생력 증가
        public float manaRegen;             // 마나 재생력 증가
    }

    public class BuffTower : TowerBase
    {
        [SerializeField] private TowerBase[] towers;
        [SerializeField] private float manaAmount;
        [SerializeField] private bool isOn => status.CurrentMana >= manaAmount;
        [SerializeField] private GameObject effectObj;
        [SerializeField] protected BuffContents buffContents;
        protected override void Start()
        {
            status = GetComponent<Status>();

            status.Init(towerInfo);

            shootTime = towerInfo.shootDelay;
        }

        protected override void Update()
        {
            shootTime += Time.deltaTime;
            ActivatedEffect();
            DoBuffForTower();
        }

        // 타워들에게 버프 적용
        protected void DoBuffForTower()
        {
            // 슛 딜레이 & 마나 검사
            if (towerInfo.shootDelay >= shootTime || isOn == false) return;

            // 범위 내 towers 선언
            List<TowerBase> towersInRage = new List<TowerBase>();

            // 모든 TowerBase를 찾음
            towers = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (TowerBase tower in towers)
            {
                // 거리 체크
                float distance = Vector3.Distance(transform.position, tower.transform.position);

                // towers 중 공격 범위 있는 TowerBase를List에 추가
                if (distance <= towerInfo.attackRange)
                {
                    towersInRage.Add(tower);
                }
            }

            // 각 타워 효과 적용
            foreach (TowerBase tower in towersInRage)
            {
                tower.BuffTower(buffContents);
                //TODO 효과 이펙트 적용
            }

            // 슛 타임 초기화
            shootTime = 0;
        }

        // 활성화 상태의 Effect On/Off
        private void ActivatedEffect()
        {
            effectObj.SetActive(isOn);
        }
    }
}