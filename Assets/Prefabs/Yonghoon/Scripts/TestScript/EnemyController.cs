using UnityEngine;
using Defend.Enemy;

namespace Defend.TestScript
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //애니메이터
        private Animator animator;

        //체력담당 컴포넌트
        private Health health;

        public Vector3 offset;

        #endregion
        void Start()
        {
            //참조
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;
        }

        private void OnDamaged(float arg0)
        {
            Debug.Log("공격 받음");
        }

        private void OnHeal(float arg0)
        {
            Debug.Log("힐 받음");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDie()
        {
            //살아있는 에너미 수 감소
            SpawnManager.enemyAlive--;

            animator.SetBool("IsDeath", true);

            //Enemy 킬
            Destroy(gameObject, 2f);
        }
    }
}