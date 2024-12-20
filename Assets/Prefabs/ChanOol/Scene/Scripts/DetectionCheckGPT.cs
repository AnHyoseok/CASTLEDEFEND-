using UnityEditor;
using UnityEngine;

public class DetectionCheckGPT : MonoBehaviour
{
    public GameObject resourcePrefab; // 생성할 자원의 프리팹
    public Transform spawnPoint; // 자원이 생성될 위치

    void Start()
    {
        spawnPoint = transform;

        // 박스 콜라이더 안에 "Tree"라는 이름의 오브젝트가 있는지 확인
        if (!HasTreeInside())
        {
            SpawnResource();
        }
        else
        {
            Debug.Log("Tree 오브젝트가 이미 있습니다. 자원을 생성하지 않습니다.");
        }
    }

    private bool HasTreeInside()
    {
        // 자신의 BoxCollider를 가져옴
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider가 필요합니다.");
            return false;
        }

        // 박스 콜라이더 영역 내의 모든 콜라이더를 검색
        Collider[] colliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.extents, boxCollider.transform.rotation);

        // "Tree"라는 이름의 오브젝트가 있는지 확인
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.name == "Tree")
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnResource()
    {
        if (resourcePrefab == null || spawnPoint == null)
        {
            Debug.LogError("resourcePrefab 또는 spawnPoint가 설정되지 않았습니다.");
            return;
        }

        // 자원 생성
        Instantiate(resourcePrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("자원이 생성되었습니다.");
    }

    void OnDrawGizmos()
    {
        // BoxCollider의 영역을 Gizmo로 표시
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(boxCollider.transform.position, boxCollider.transform.rotation, boxCollider.transform.lossyScale);
            Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
        }
    }
}

