using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // 생성할 오브젝트의 프리팹
    public float width = 10f; // x축 방향 크기
    public float depth = 66f; // z축 방향 크기
    private Vector3 currentPosition; // 현재 오브젝트 위치
    [SerializeField] private int maxTrees = 10; // 나무가 10 개면 더이상 생성X
    [SerializeField] private float spawnDelay = 5f; // 나무 생성 딜레이

    void Start()
    {
        // 현재 오브젝트의 위치를 가져옵니다.
        currentPosition = transform.position;

        StartCoroutine(SpawnTrees());
    }

    public IEnumerator SpawnTrees()
    {
        for (int i = 0; i < maxTrees; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            SpawnTree();
        }
    }

    public void SpawnTree()
    {
        // 랜덤한 x축 위치를 생성합니다.
        float randomX = Random.Range(currentPosition.x - width / 2, currentPosition.x + width / 2);
        //Debug.Log($"Random.Range : {randomX}");

        // 랜덤한 z축 위치를 생성합니다.
        float randomZ = Random.Range(currentPosition.z - depth / 2, currentPosition.z + depth / 2);
        //Debug.Log($"Random.Range : {randomZ}");

        // y축 위치는 현재 위치와 동일하게 유지합니다.
        float randomY = currentPosition.y;
        //Debug.Log($"Random.Range : {randomY}");

        // 새로운 랜덤 위치를 생성합니다.
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        // 새로운 오브젝트를 생성합니다.
        Instantiate(treePrefab, spawnPosition, Quaternion.identity);
    }

    // Scene 뷰에서 테두리를 그리는 메서드
    private void OnDrawGizmos()
    {
        // 현재 위치를 기준으로 테두리를 계산합니다.
        Vector3 center = transform.position;
        Vector3 size = new Vector3(width, 0, depth);

        // 기즈모 색상 설정
        Gizmos.color = Color.green;

        // 테두리 박스를 그립니다.
        Gizmos.DrawWireCube(center, size);
    }
}
