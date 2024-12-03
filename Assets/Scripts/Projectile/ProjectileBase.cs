using Defend.TestScript;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 발사체의 기능을 정의한 상위 클래스
/// </summary>
namespace Defend.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Transform target;                    // 목표물
        [SerializeField] protected Vector3 targetPosition;              // 목표물
        [SerializeField] protected ProjectileInfo projectileInfo;       // 발사체 정보
        [SerializeField] protected Vector3 offset;

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {

        }

        // 타겟과 발사체 정보 초기화
        public virtual void Init(ProjectileInfo _projectileInfo, Transform closestTarget)
        {
            target = closestTarget;
            offset = target.gameObject.GetComponent<EnemyController>().offset;
            targetPosition = target.position + offset;
            projectileInfo = _projectileInfo;
        }

        // 타겟에 Projectile Effect 생성
        protected virtual void Hit()
        {
            // Projectile Effect 생성
            GameObject effect = Instantiate(projectileInfo.effectPrefab, transform.position, Quaternion.identity);
            // Projectile Effect 삭제 예약
            Destroy(effect, projectileInfo.effectTime);
            // Projectile 삭제 
            Destroy(this.gameObject);
        }

        // 타겟의 폭발 범위 만큼 범위공격
        protected void HitOnRange()
        {
            // EnemyController 컴포넌트를 가진 Object 찾기
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (var obj in enemies)
            {
                // 거리 체크
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance <= projectileInfo.attackRange)
                {
                    Health health = obj.GetComponent<Health>();
                    if (health != null)
                    {
                        // 데미지 주기
                        health.TakeDamage(projectileInfo.attack);
                    }
                }
            }
        }

        // 타겟만 공격
        protected void HitOnTarget()
        {
            // Health 컴포넌트 접근
            Health health = target.GetComponent<Health>();
            // 데미지 주기
            health.TakeDamage(projectileInfo.attack);
        }


        // 공격범위 기즈모
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, projectileInfo.attackRange);
        }
    }
}