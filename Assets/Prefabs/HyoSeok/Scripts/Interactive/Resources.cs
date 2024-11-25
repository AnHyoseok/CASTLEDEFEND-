using System.Collections;
using System.Resources;
using UnityEngine;
namespace Defend.Interactive{
 
    public class Resources : MonoBehaviour
    {
        public int health = 50;  // 돌의 체력
        public int resourceAmount = 10;  //얻는자원


        public GameObject resourcePickupEffect;  // 자원이 떨어질 때 효과
        private bool isDamaged = false;  // 데미지를 받는 중인지 여부

        private void OnTriggerEnter(Collider other)
        {
            // 손과 닿으면
            if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && !isDamaged)
            {
                StartCoroutine(TakeDamage(10));  // 손으로 때릴 때마다 10 데미지
            }
        }

        //1초에 한대씩때리기
        private IEnumerator TakeDamage(int damage)
        {
            isDamaged = true; 
            health -= damage;
            Debug.Log($"health = {health}");
            // 자원 얻기
            GiveResource();
            
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(1f);  

            isDamaged = false;  
        }
        void GiveResource()
        {
            // 자원 생성 효과이펙트(조그만한 자원이 떨어지는느낌?) 그리고 1초뒤 제거?
            if (resourcePickupEffect != null)
            {
               GameObject effectGo= Instantiate(resourcePickupEffect, transform.position, Quaternion.identity);
                Destroy(effectGo,1f);
            }

            // 플레이어에게 전달
            ResourceManager.Instance.AddResources(resourceAmount);
        }
    }

}