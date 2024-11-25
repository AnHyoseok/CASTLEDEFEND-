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
        public virtual void Init(ProjectileInfo _projectileInfo, Transform _target)
        {
            target = _target;
            projectileInfo = _projectileInfo;
        }



        // 타겟을 맞춤
        protected virtual void Hit()
        {
            //Hit 효과
            //GameObject effectGo = Instantiate(bulletImpactPrefab, this.transform.position, Quaternion.identity);
            //Destroy(effectGo, 2f);

            ////타겟에 데미지 준다
            //Damage(target);

            ////탄환 게임오브젝트 kill (Destroy)
            //Destroy(this.gameObject);

            Debug.Log("HIT");
        }
    }
}