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
        [SerializeField] protected ProjectileInfo projectileInfo;       // 발사체 정보

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
            projectileInfo = _projectileInfo;
        }

        // 타겟을 맞춤
        protected virtual void Hit()
        {
            // Projectile Effect 생성
            GameObject effect = Instantiate(projectileInfo.effectPrefab, transform.position, Quaternion.identity);
            // Projectile Effect 삭제 예약
            Destroy(effect, projectileInfo.effectTime);
            // Projectile 삭제 
            Destroy(this.gameObject);
        }
    }
}