using UnityEngine;
using System.Collections;

public class CrystalSpawner : MonoBehaviour
{
    #region Variables

    public GameObject crystalPrefab;    // Cave �տ��� ������ ������

    public float spawnDistance = 2f;    // ���� ������Ʈ������ �Ÿ�

    public float spawnInterval = 3f;    // ���� �ֱ�

    [SerializeField] private bool isSpawn;   // �����Ǿ��ִ��� üũ

    #endregion

    private void Start()
    {
  
    }

    private void Update()
    {
        //Ÿ�̸� ���
        StartCoroutine(SpawnObjectRoutine(spawnInterval));
    }

    

    private IEnumerator SpawnObjectRoutine(float interval)
    {
        while (true)
        {
            if (isSpawn == false)
            {
                // ���� ������Ʈ �� ��ġ ���
                Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

                // ������Ʈ ����
                Instantiate(crystalPrefab, spawnPosition, Quaternion.identity);

                // ���� ������� ���
                yield return new WaitForSeconds(interval);

                isSpawn = true;
            }
        }
    }
}

