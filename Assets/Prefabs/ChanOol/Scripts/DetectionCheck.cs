using UnityEngine;
using System.Collections;

public class DetectionCheck : MonoBehaviour
{
    private BoxCollider boxCollider;

    //private bool treeFound = false;

    //private bool canBuild = false;

    private bool treeSpawned = false; // 나무가 생성되었는지 여부를 추적

    //private TreeSpawner treeSpawner;

    public GameObject treePrefab;



    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        //treeSpawner = GetComponent<TreeSpawner>();
    }

    void Update()
    {
        // 부모 오브젝트인 TreeSpawner 스크립트가 끝나고나서부터 실행
        //if (treeSpawner.detectionCheckActive == true)
        {
            // 콜라이더 안에 있는 모든 오브젝트 가져오기
            Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, boxCollider.transform.rotation);

            // "Tree"라는 이름의 오브젝트가 있는지 확인
            bool treeDetected = false;
            foreach (var collider in colliders)
            {
                if (collider.gameObject.name=="Tree(Clone)")
                {
                    treeDetected = true;
                    break;
                }
            }

            // 나무가 없고, 나무가 아직 생성되지 않았다면 SpawnTree 호출
            if (!treeDetected && !treeSpawned)
            {
                treeSpawned = true; // 나무 생성 상태 설정
                StartCoroutine(SpawnTree());
            }
            else if(treeDetected) 
            {
                treeSpawned = false; // 나무가 발견되면 생성 상태 초기화
            }
        }
    }

    public IEnumerator SpawnTree()
    {
        yield return new WaitForSeconds(5f); // 5초 후에 나무 생성

        if (treePrefab == null)
        {
            Debug.Log("treePrefab == null");
        }
        Instantiate(treePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
    }
}

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            isTreeInside = true;
            Debug.Log(isTreeInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            isTreeInside = false;
            Debug.Log(isTreeInside);
        }
    }

    private void OnDestroy()
    {
        isTreeInside = false;
    }*/

