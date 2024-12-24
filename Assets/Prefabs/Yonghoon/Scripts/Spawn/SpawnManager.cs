using System;
using System.Collections;
using UnityEngine;

namespace Defend.Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        //필드
        #region Variable
        //스폰 위치(시작 위치)
        public Transform startPoint;

        //스폰 매니저
        public Transform spawnManager;

        //스폰 타이머
        public float spawnTimer = 5f;
        private float countdown = 0f;

        //스폰중이면 true, 아니면 false
        [SerializeField] private bool isSpawn;

        //Wave 데이터 셋팅
        public WaveData[] waves;
        private int waveCount;  //웨이브 카운트

        //살아있는 enemy의 개수
        public static int enemyAlive;
        //public int enemyAlive;

        #endregion

        void Start()
        {
            //초기화 - 시작시 대기 시간
            countdown = 2f;
            isSpawn = false;
            enemyAlive = 0;
            waveCount = 0;
        }

        void Update()
        {
            //가드절 (웨이브 세팅 수와 웨이브 카운트가 같거나 카운트가 커지면 실행하지않음
            if (waves.Length <= waveCount) return;

            if (countdown <= 0f) 
            {
                //타이머 명령
                StartCoroutine(SpawnWave());

                //초기화
                countdown = spawnTimer;
            }

            if (isSpawn == false)
            {
                countdown -= Time.deltaTime;
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            }
            //UI            
            //countdownText.text = Mathf.Round(countdown).ToString();

        }

        //시작지점 위치에 Enemy를 생성
        private void SpawnEnemy(GameObject enemyPrefab)
        {
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, spawnManager);
            enemyAlive++;
        }

        //Wave Data에 맞게 적 스폰
        IEnumerator SpawnWave()
        {
            isSpawn = true;

            //0 -> WaveData에 데이터 추가시 카운트변수 설정
            WaveData wave = waves[waveCount];

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(wave.delayTime);
            }

            waveCount++;

            isSpawn = false;
        }
    }
}