using Defend.item;
using MyVrSample;
using System.Collections;
using System.Resources;
using UnityEngine;
namespace Defend.Interactive
{

    public class Resources : MonoBehaviour
    {
        public enum ResourceTypeEnum
        {
            Rock,
            Tree,
            Money
                
        }

        [System.Serializable]
        public class ResourceType
        {
            public ResourceTypeEnum name;        // 자원 이름 
            public float amount;       // 자원 양
            public float health;       // 자원 별 체력
            public GameObject resourceItem;  // 자원 드랍아이템
           
        }
        #region
      

        private bool isDamaged = false;  // 데미지를 받는 중인지 여부
        public ResourceType[] resourceTypes;  // 자원 타입 배열
        private ResourceType currentResourceType;    // 현재 자원 타입

        //타격 사운드
        public AudioClip  hitSound;
        private AudioSource audioSource;

        //제거 이펙트
        public GameObject destroyEffect;


        #endregion

        private void Awake()
        {
            // 첫 번째 자원 타입을 기본으로 설정
            if (resourceTypes.Length > 0)
            {
                currentResourceType = resourceTypes[0];
            }
        }
        private void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
           
        }

        private void OnTriggerEnter(Collider other)
        {
            // 충돌한 오브젝트의 이름과 현재 오브젝트의 이름을 기반으로 자원 타입 설정
            string otherName = other.gameObject.name;
            string currentName = gameObject.name;

            if (!isDamaged &&
                ((otherName == "Axe" && currentName == "Tree") ||
                 (otherName == "PickAxe" && currentName == "Rock")))
            {
                SetCurrentResourceType(currentName);
                StartCoroutine(Shake());
                StartCoroutine(TakeDamage(10)); 
            }
        }

        //1초에 한대씩때리기
        private IEnumerator TakeDamage(float damage)
        {
            isDamaged = true;
            currentResourceType.health -= damage;
            Debug.Log($"{currentResourceType.name} health = {currentResourceType.health}");

            // 자원 생성 (미니사이즈)
            if (currentResourceType.resourceItem != null)
            {
                //위치 다시잡아야됌 ======================================================================================================
                GameObject dropitem = Instantiate(currentResourceType.resourceItem, transform.position, Quaternion.identity);
                DropItem item = dropitem.GetComponent<DropItem>();
                item.amount = currentResourceType.amount;
                item.resourceName = currentResourceType.name.ToString();

            }

            PlayHitSound();
            if (currentResourceType.health <= 0)
            {

                // 흔들림 효과와 사운드가 재생될 시간을 주기 위해 대기
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
                //제거 이펙트
               GameObject detheffect= Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(detheffect,1f);
            }

            yield return new WaitForSeconds(1f);

            isDamaged = false;
        }

        //오브젝트 흔들기
        IEnumerator Shake()
        {
            float t = 1f;
            float shakePower = 1f;
            Vector3 origin = transform.position;
           
            while (t > 0f)
            {
                t -= 0.05f;
                transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * t;
                yield return null;
            }

            transform.position = origin;
        }
       // //자원흭득
       //public void GiveResource()
       // {
       //     // 플레이어에게 전달
       //     ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
       // }

        // 자원 타입에 따른 현재 자원 설정
        public void SetCurrentResourceType(string resourceName)
        {
            foreach (var resourceType in resourceTypes)
            {
                if (resourceType.name.ToString().Equals(resourceName, System.StringComparison.OrdinalIgnoreCase))
                {
                    currentResourceType = resourceType;
                    return;  // 타입을 찾았으면 바로 리턴
                }
            }
        }

        //히트 사운드
        public void PlayHitSound()
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }

}