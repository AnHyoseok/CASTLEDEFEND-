using UnityEngine;
using UnityEngine.UI;

namespace Defend.Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region Variables
        //체력
        private float health;
        [SerializeField] private float startHealth = 100;

        //보상 금액
        [SerializeField]
        private int rewardGold = 50;

        //죽었는지 확인
        private bool isDeath;

        //HealthBar
        //public Image healthBar;
        #endregion

        private void Start()
        {
            //초기화
            health = startHealth;
            isDeath = false;
        }

        //데미지 처리
        public void TakeDamage(float damage)
        {
            if (isDeath) return;
            health -= damage;
            //Debug.Log($"health: {health}");

            //healthBar
            //healthBar.fillAmount = health / startHealth;

            if (health <= 0)
            {
                Die();
            }
        }

        //죽음 처리
        void Die()
        {
            isDeath = true;
            //죽는 이펙트 처리

            //리워드로 50 Gold 지급

            //Enemy count --

            //kill
            Destroy(gameObject);
        }
    }
}