using Defend.TestScript;
using System;
using UnityEngine;
using static Defend.Utillity.AudioUtility;
/// <summary>
/// Debuff를 거는 발사체 기능 구현
/// </summary>
namespace Defend.Projectile
{
    public class Debuffer<T> : TargetProjectile where T : Component
    {
        [SerializeField] protected T targetComponent;             // 제네릭 컴포넌트
        [SerializeField] protected bool isDebuff = false;         // 디버프 상태

        // 디버프 적용 동작
        protected virtual void DoDebuffAction(T component) { }
        // 원상복구 동작
        protected virtual void UndoDebuffAction(T component) { }

        protected override void Start()
        {
            // 대상의 컴포넌트를 가져옴
            targetComponent = target.GetComponent<T>();
            if (targetComponent == null)
            {
                Debug.Log($"NULL Component!");
            }
        }

        protected override void Update()
        {
            // 발사한 타워가 판매 또는 업그레이드되면 발사체 파괴
            if (projectileInfo.tower == null)
            {
                Destroy(this.gameObject);
            }

            base.Update();
            CheckDistanceFromTower();
            CheckHealth();
        }

        // 파괴될 때 시행
        private void OnDestroy()
        {
            // 디버프 해제
            RemoveDebuff(UndoDebuffAction);
        }

        protected override void Hit()
        {

        }
        private void CheckHealth()
        {
            if (target.GetComponent<Health>().CurrentHealth <= 0)
                Destroy(this.gameObject);
        }

        // 디버프 적용
        public void ApplyDebuff(Action<T> applyAction)
        {
            applyAction(targetComponent); // 디버프 동작 수행
            isDebuff = true;

            // Projectile Sound 생성
            if (projectileInfo.sfxClip != null)
            {
                CreateSFX(projectileInfo.sfxClip, transform.position, AudioGroups.EFFECT);
            }
        }

        // 디버프 제거
        public void RemoveDebuff(Action<T> removeAction)
        {
            removeAction(targetComponent); // 디버프 해제 동작 수행
            isDebuff = false;
        }

        // 타워와의 거리 확인
        protected virtual void CheckDistanceFromTower()
        {
            if (targetComponent == null) return;
            if (projectileInfo.tower == null) return;

            float distance = Vector3.Distance(transform.position, projectileInfo.tower.transform.position);
            bool inAttackRange = distance <= projectileInfo.attackRange;

            // 타워 범위 내에 있고, 디버프가 적용되지 않았다면
            if (inAttackRange == true && isDebuff == false)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                ApplyDebuff(DoDebuffAction);
            }
            // 타워 범위 밖에 있고, 디버프가 적용되었다면
            else if (inAttackRange == false && isDebuff == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                RemoveDebuff(UndoDebuffAction);
            }
            // 타워 범위 내에 있고, 디버프가 적용되어 있는 경우
            // 타워 범위 밖에 있고, 디버프가 적용되지 않은 경우
            else
            {
                //Debug.Log($"디버프 타워 예외 발생 \n inAttackRange = {inAttackRange} \n isDebuff = {isDebuff}");
            }
        }
    }
}
