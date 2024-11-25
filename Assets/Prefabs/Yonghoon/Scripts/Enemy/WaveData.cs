using UnityEngine;

namespace Defend.Enemy
{
    //Wave Data를 관리하는 직렬화클래스
    [System.Serializable]
    public class WaveData
    {
        public GameObject enemyPrefab;      //생성되는 Enemy프리팹
        public int count;                   //생성되는 Enemy개수
        public float delayTime;             //생성지연시간
    }
}