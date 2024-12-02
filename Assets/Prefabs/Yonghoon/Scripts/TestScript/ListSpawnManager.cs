using System.Collections;
using UnityEngine;

namespace Defend.Enemy
{
    public class ListSpawnManager : MonoBehaviour
    {
        #region Variable

        // 스폰 위치를 정의하는 시작 지점 (Transform)
        public Transform startPoint;

        // 스폰된 몬스터를 정리할 부모 오브젝트
        public Transform spawnManager;

        // 웨이브 간 대기 시간
        public float spawnTimer = 180f;
        private float countdown = 0f; // 다음 웨이브 시작까지 남은 시간

        // 현재 스폰 중인지 여부를 나타내는 상태 플래그
        [SerializeField] private bool isSpawn;

        // 웨이브 데이터를 배열로 관리
        public ListWaveData[] waves;
        private int waveCount; // 현재 웨이브 번호

        // 현재 살아있는 몬스터의 총 개수 (모든 웨이브 공유)
        public static int enemyAlive;

        #endregion

        void Start()
        {
            // 초기 설정
            // 첫 웨이브 시작 대기 시간 설정
            //countdown = 2f;             //2초뒤에 첫 웨이브 시작
            countdown = spawnTimer;     //spawnTimer만큼 지나고 웨이브 시작

            isSpawn = false;      // 처음엔 스폰 상태가 아님
            enemyAlive = 0;       // 시작 시 살아있는 몬스터는 없음
            waveCount = 0;        // 첫 번째 웨이브부터 시작
        }

        void Update()
        {
            // 모든 웨이브를 다 스폰했으면 종료
            if (waves.Length <= waveCount) return;

            #region Basic Wave Logic (Timer)

            // 카운트다운이 0 이하가 되면 새로운 웨이브 시작
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave()); // 웨이브 스폰 코루틴 호출
                countdown = spawnTimer;     // 다음 웨이브를 위한 타이머 리셋
            }

            // 스폰 중이 아니고, 웨이브 몬스터를 전부 잡았을때 카운트다운 감소
            if (!isSpawn && enemyAlive == 0)
            {
                countdown -= Time.deltaTime; // 타이머를 감소
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // 타이머 값 제한
            }

            #endregion

            #region new Wave Logic (Monster Alive Check)

            /*
            // 살아있는 몬스터가 없고 스폰이 끝났으면 다음 웨이브 시작
            if (enemyAlive == 0 && !isSpawn)
            {
                StartCoroutine(SpawnWave());
            }
            */

            #endregion


        }

        // 몬스터 프리팹을 스폰하는 함수
        private void SpawnEnemy(GameObject enemyPrefab)
        {
            // 지정된 위치에서 몬스터를 생성하고, spawnManager 아래로 배치
            Instantiate(enemyPrefab, startPoint.position, Quaternion.identity, spawnManager);

            // 살아있는 몬스터 수 증가
            enemyAlive++;
        }

        // 웨이브를 스폰하는 코루틴 함수
        IEnumerator SpawnWave()
        {
            isSpawn = true; // 스폰 시작 상태로 전환

            // 현재 웨이브 데이터를 가져옴
            ListWaveData wave = waves[waveCount];
            //Debug.Log(wave);
            // 웨이브 내의 모든 몬스터 데이터를 순회
            foreach (var enemyData in wave.enemies)
            {
                //Debug.Log(enemyData);
                // 각 몬스터의 개수만큼 스폰
                for (int i = 0; i < enemyData.count; i++)
                {
                    SpawnEnemy(enemyData.enemyPrefab); // 몬스터 스폰
                    yield return new WaitForSeconds(enemyData.delayTime); // 다음 몬스터 생성까지 대기
                }
            }

            waveCount++; // 웨이브 번호 증가
            isSpawn = false; // 스폰 종료 상태로 전환
        }

        public void SkipTimer()
        {
            countdown = 5f;
        }
    }
}
