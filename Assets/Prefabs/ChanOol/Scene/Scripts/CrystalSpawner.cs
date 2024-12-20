using UnityEngine;
using System.Collections;

public class CrystalSpawner : MonoBehaviour
{
    #region Variables

    public GameObject crystalPrefab;    // 스폰할 프리팹
    public float spawnInterval = 3f;    // 스폰 주기
    public int spawnCount = 3;          // 스폰 갯수
    [SerializeField] private bool isSpawn;   // isSpawn이 true 일때만 스폰

    #endregion

    private void Start()
    {
        //타이머 명령
        StartCoroutine(SpawnObjectRoutine(spawnInterval));
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnObjectRoutine(float interval)
    {
        while (true)
        {
            if (isSpawn == false)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    // 오브젝트 생성
                    Instantiate(crystalPrefab, transform.position, Quaternion.identity);

                    // 다음 실행까지 대기
                    yield return new WaitForSeconds(interval);
                }
                isSpawn = true;
            }
            else
            {
                // 플래그가 true라면 바로 다음 프레임으로 대기
                yield return null;
            }
        }
    }
}

