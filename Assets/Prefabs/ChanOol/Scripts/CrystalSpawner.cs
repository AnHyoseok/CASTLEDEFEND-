using UnityEngine;
using System.Collections;

public class CrystalSpawner : MonoBehaviour
{
    #region Variables

    public GameObject crystalPrefab;    // Cave 앞에서 생성할 프리팹

    public float spawnDistance = 2f;    // 현재 오브젝트에서의 거리

    public float spawnInterval = 3f;    // 생성 주기

    [SerializeField] private bool isSpawn;   // 생성되어있는지 체크

    #endregion

    private void Start()
    {
  
    }

    private void Update()
    {
        //타이머 명령
        StartCoroutine(SpawnObjectRoutine(spawnInterval));
    }

    

    private IEnumerator SpawnObjectRoutine(float interval)
    {
        while (true)
        {
            if (isSpawn == false)
            {
                // 현재 오브젝트 앞 위치 계산
                Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

                // 오브젝트 생성
                Instantiate(crystalPrefab, spawnPosition, Quaternion.identity);

                // 다음 실행까지 대기
                yield return new WaitForSeconds(interval);

                isSpawn = true;
            }
        }
    }
}

