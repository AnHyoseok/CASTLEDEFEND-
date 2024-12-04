using Defend.Player;
using Defend.Interactive;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

namespace Defend.item
{
    public class DropItem : MonoBehaviour
    {
        #region Variables
        private PlayerState playerState;
        public string resourceName; // 자원이름
        public float amount; // 자원 양

        //아이템 흭득 사운드
        public AudioClip GetItemSound;
        private AudioSource audioSource;
       
        #endregion

        private void Awake()
        {
            Collider collider = GetComponent<Collider>();
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState를 찾기
            audioSource = gameObject.AddComponent<AudioSource>();
          

        }
       

        private void Update()
        {
            MagnetItem();
        }


        //플레이어가 거리에 있으면 플레이어에게 이동
        void MagnetItem()
        {
            if (playerState != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerState.transform.position);
                if (distanceToPlayer < ResourceManager.distance)
                {
                    Vector3 direction = (playerState.transform.position - transform.position).normalized;
                    transform.position += direction * ResourceManager.speed * Time.deltaTime;
                }
            }
        }

        void GetResource()
        {
            // 플레이어에게 전달
            ResourceManager.Instance.AddResources(amount, resourceName);
        }

        //닿으면 제거
        private void OnTriggerEnter(Collider other)
        {
            

            if (other.CompareTag("Player"))
            {
                GetResource();
                audioSource.clip = GetItemSound;
                audioSource.Play();
                Destroy(gameObject, 0.1f);

            }

        }

    }


}