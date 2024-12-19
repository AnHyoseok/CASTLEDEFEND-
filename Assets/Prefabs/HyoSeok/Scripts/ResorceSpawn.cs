using UnityEngine;
using System.Collections;
namespace Defend.Interactive
{
    public class ResorceSpawn : MonoBehaviour
    {
        #region Variables
        public bool treeSpawned = true;
        public GameObject treePrefab;
        #endregion

        private void Update()
        {

              StartCoroutine(SpawnTree());
        }

        public IEnumerator SpawnTree()
        {
            if (treeSpawned == true)
            {
                yield return new WaitForSeconds(5f); // 5�� �Ŀ� ���� ����

                if (treePrefab == null)
                {
                    Debug.Log("treePrefab == null");
                }
                GameObject tree = Instantiate(treePrefab, transform.position, Quaternion.identity);
               
            }
        }

     
    }
}