using UnityEngine;
using System.Collections;
namespace Defend.Interactive
{
    public class RockSpawner : MonoBehaviour
    {
        #region Variables
        [SerializeField] int spawntime = 30;
        public GameObject rockPrefab;
        private GameObject currentObject;
        private bool isRespawning = false;
        #endregion

        void Start()
        {
            SpawnObject();
        }

        void SpawnObject()
        {
            currentObject = Instantiate(rockPrefab, transform.position, Quaternion.identity);
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