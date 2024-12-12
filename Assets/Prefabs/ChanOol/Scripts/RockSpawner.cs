using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour
{
    // 생성시킬 프리팹
    public GameObject rockPrefab;

    // 생성시킬 위치 다섯 군데
    private Transform[] spawnPosition = new Transform[4];

    // 임시 랜덤번호
    private int tempIndex;

    // 중복체크 완료한 랜덤번호
    private int[] randomIndex = new int[4];

    /*// 자식 스크립트 참조
    private DetectionCheck detectionCheck;

    // 
    private float randomCooldown;*/


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*// 자식 스크립트 참조
        detectionCheck = GetComponentInChildren<DetectionCheck>();*/

        // 자동으로 자식들 포지션 참조
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            spawnPosition[i] = transform.GetChild(i).transform;
        }


        // 첫번째 인덱스는 바로 할당
        randomIndex[0] = Random.Range(0, 4);

        // 두번째 인덱스부터는 중복검사
        for (int i = 1; i < 4; i++)
        {
            do
            {
                tempIndex = Random.Range(0, 4);

                // 중복인지 확인
                bool isDuplicate = false;
                for (int j = 0; j < i; j++) // 이미 할당된 값만 비교
                {
                    if (tempIndex == randomIndex[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                // 중복이 아니라면 반복 종료
                if (!isDuplicate)
                    break;

            } while (true); // 중복이면 계속 랜덤 값 생성

            // 중복검사 통과하면 할당
            randomIndex[i] = tempIndex;
        }

        StartCoroutine(SpawnTrees());

    }

    // Update is called once per frame
    void Update()
    {
        /*// 콜라이더 안에
        if (detectionCheck.isRockInside == false)
        { 
            
        }*/

    }

    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < randomIndex.Length; i++)
        {
            yield return new WaitForSeconds(8f);

            Instantiate(rockPrefab, spawnPosition[randomIndex[i]].position, Quaternion.identity);
        }
    }
}