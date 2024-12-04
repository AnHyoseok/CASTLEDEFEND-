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
        public string resourceName; // �ڿ��̸�
        public float amount; // �ڿ� ��

        //������ ŉ�� ����
        public AudioClip GetItemSound;
        private AudioSource audioSource;
       
        #endregion

        private void Awake()
        {
            Collider collider = GetComponent<Collider>();
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
            audioSource = gameObject.AddComponent<AudioSource>();
          

        }
       

        private void Update()
        {
            MagnetItem();
        }


        //�÷��̾ �Ÿ��� ������ �÷��̾�� �̵�
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
            // �÷��̾�� ����
            ResourceManager.Instance.AddResources(amount, resourceName);
        }

        //������ ����
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