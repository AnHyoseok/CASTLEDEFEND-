using UnityEngine;
using System.Collections;
namespace Defend.Interactive
{
    public class ResorceSpawn : MonoBehaviour
    {
        #region Variables
        [SerializeField] int spawntime = 5;
        public GameObject treePrefab;
        private GameObject currentObject;
        private bool isRespawning = false; 
        #endregion

        void Start()
        {
            SpawnObject();
        }

        void SpawnObject()
        {
            currentObject = Instantiate(treePrefab, transform.position, Quaternion.identity);
            isRespawning = false; 
        }

        void Update()
        {
        
            if (currentObject == null && !isRespawning)
            {
                isRespawning = true; 
                Invoke("RespawnObject", spawntime);
            }
        }

        void RespawnObject()
        {
            SpawnObject();
        }


    }
}