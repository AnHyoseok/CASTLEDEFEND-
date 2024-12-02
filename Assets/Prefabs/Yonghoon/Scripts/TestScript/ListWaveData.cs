using UnityEngine;
using System.Collections.Generic; // 리스트 자료구조를 사용하기 위해 추가

namespace Defend.Enemy
{
    // 개별 몬스터 데이터를 관리하는 클래스
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab;      // 스폰할 몬스터의 프리팹
        public int count;                   // 해당 몬스터의 생성 개수
        public float delayTime;             // 몬스터 생성 간의 지연 시간
    }

    // 한 웨이브의 데이터를 관리하는 클래스
    [System.Serializable]
    public class ListWaveData
    {
        public List<EnemyData> enemies;     // 웨이브에 포함된 여러 몬스터 데이터를 리스트로 관리
    }
}
