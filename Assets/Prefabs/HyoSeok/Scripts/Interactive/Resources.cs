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
            public GameObject resourcePickupEffect;  // 자원이 떨어질 때 효과
           
        }
        #region
        //참조
        private ItemDrop ItemDrop;

        private bool isDamaged = false;  // 데미지를 받는 중인지 여부
        public ResourceType[] resourceTypes;  // 자원 타입 배열
        private ResourceType currentResourceType;    // 현재 자원 타입

        //타격 사운드
        public AudioClip  hitSound;
        private AudioSource audioSource;
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
            // 손과 닿으면 // 오브젝트 이름으로 수정할것 !!
            //무기시스템 만들수있으니 마지막에 수정함
            if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && !isDamaged)
            {
                // 충돌한 오브젝트의 이름을 기반으로 자원 타입 설정
                SetCurrentResourceType(gameObject.name);
                StartCoroutine(Shake());
                StartCoroutine(TakeDamage(10));  // 손으로 때릴 때마다 10 데미지
   
            }
        }

        //1초에 한대씩때리기
        private IEnumerator TakeDamage(float damage)
        {
            isDamaged = true;
            currentResourceType.health -= damage;
            Debug.Log($"{currentResourceType.name} health = {currentResourceType.health}");
        

            // 자원 얻기
            GiveResource();
            PlayHitSound();
            if (currentResourceType.health <= 0)
            {

                // 흔들림 효과와 사운드가 재생될 시간을 주기 위해 대기
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
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

        void GiveResource()
        {
            // 자원 생성 효과이펙트(조그만한 자원이 떨어지는느낌?) 그리고 1초뒤 제거?
            if (currentResourceType.resourcePickupEffect != null)
            {
                GameObject effectGo = Instantiate(currentResourceType.resourcePickupEffect, transform.position, Quaternion.identity);
                Destroy(effectGo, 2f);
            }

            // 플레이어에게 전달
            ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }

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