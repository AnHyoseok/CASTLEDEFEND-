using Defend.Enemy;
using Defend.Projectile;
using Defend.Utillity;
using System.Collections.Generic;
using UnityEngine;
using Defend.TestScript;
using System.Linq;
using System.Collections;
/*
기본타워 => 타겟공격
스플래시타워 => 지점공격
멀티공격타워 => 타겟공격
레이저타워 => 타겟공격
슬로우타워 => 직접 공격은 하지 않고 일정 범위에 해당 효과 적용
강화타워   => 직접 공격은 하지 않고 일정 범위에 해당 효과 적용
방깎타워   => 직접 공격은 하지 않고 일정 범위에 해당 효과 적용
*/
/// <summary>
/// Tower의 공통적인 기능을 정의한 상위 클래스
/// </summary>
namespace Defend.Tower
{
    public abstract class TowerBase : MonoBehaviour
    {
        #region Variables
        // 타겟
        [SerializeField] List<Transform> targets;           // 타겟들
        public Transform currentTarget;                     // 현재 가장 가까운 타겟
        #region Layer 및 collider 사용 => 미사용
        //public List<LayerMask> targetLayerList;             // 타겟 오브젝트의 레이어
        #endregion

        // 발사
        public Transform firePoint;                         // 발사체 시작점
        [SerializeField] protected float shootTime;         // 슛 타임 카운트

        // 타워 정보
        [SerializeField] protected TowerInfo towerInfo;

        // 컴포넌트
        protected Animator animator;
        protected Status status;
        [SerializeField] protected ParticleSystem buffEffect;
        [SerializeField] protected ParticleSystem debuffEffect;
        [SerializeField] protected GameObject destroyEffect;
        #endregion

        #region Variables For Test
        [SerializeField] public Gizmo gizmo;
        LineRenderer lineRenderer;                          // 라인 랜더러
        #endregion

        protected virtual void Awake()
        {
            status = GetComponent<Status>();
            status.Init(towerInfo);
        }

        protected virtual void Start()
        {
            // 참조
            animator = GetComponent<Animator>();

            #region Layer 및 collider 사용 => 미사용
            // Layer 설정
            //targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_ENEMY));
            //targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_BOSS));
            #endregion

            // 일정 주기로 타겟 탐색
            InvokeRepeating(nameof(SetClosestTarget), 0f, towerInfo.detectDelay);

            #region Test를 위한 시각화 => LineRenderer 초기화, Gizmo
            {
                // 기즈모 색상 초기화
                gizmo.gizmoColor = Color.green;
                // 기즈모 범위 초기화
                gizmo.sphereRadius = towerInfo.attackRange;
                // 라인랜더러 길이 초기화
                gizmo.lineLength = towerInfo.attackRange;

                // LineRenderer 컴포넌트를 가져오거나 추가
                lineRenderer = GetComponent<LineRenderer>();
                if (lineRenderer == null)
                    lineRenderer = gameObject.AddComponent<LineRenderer>();

                // LineRenderer 초기 설정
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.positionCount = 2; // 시작점과 끝점
                lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 셰이더
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
            #endregion
        }

        protected virtual void Update()
        {
            SetRotationToTarget(); // 매 프레임마다 타겟을 바라보도록 회전
            shootTime += Time.deltaTime;

            //Shoot();
            // TEST
            //DrawLine();            // 타겟 방향으로 라인 그리기
        }

        // 타겟의 방향으로 회전
        protected virtual void SetRotationToTarget()
        {
            if (currentTarget != null)
            {
                // 타겟 설정
                Vector3 targetPosition = currentTarget.position;

                // 타겟의 y 좌표를 현재 오브젝트의 y 좌표로 고정
                targetPosition.y = transform.position.y;

                // 현재 오브젝트에서 타겟을 향하는 방향 계산
                Vector3 direction = targetPosition - transform.position;

                // 타겟을 바라보는 회전 계산
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 서서히 회전 (Slerp 사용)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, towerInfo.rotationSpeed * Time.deltaTime);

                // 회전이 거의 완료되었는지 체크
                if (Quaternion.Angle(transform.rotation, targetRotation) <= 15.0f || towerInfo.rotationSpeed == 0)
                {
                    Shoot(); // 회전 완료 후 슛 실행
                }
            }
        }

        // 범위 내의 타겟을 갱신
        protected List<Transform> UpdateTargets()
        {
            // 기존 타겟 초기화
            targets.Clear();

            // 반환할 타겟들 = 범위 내 타겟
            List<Transform> tempTarget = new List<Transform>();

            // EnemyController를 가진 Object를 찾음
            var enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

            // Objects 중 공격 범위 있는 Object를 반환할 List에 추가
            foreach (EnemyController target in enemies)
            {
                // 거리 체크
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= towerInfo.attackRange)
                {
                    tempTarget.Add(target.transform);
                }
            }

            #region Layer 및 collider 사용 => 미사용
            // targetLayerList에서 여러 레이어를 비트 연산으로 결합
            //int combinedLayerMask = 0;
            //foreach (var layerMask in targetLayerList)
            //{
            //    combinedLayerMask |= layerMask.value;  // 비트 연산으로 레이어 결합
            //}

            // 공격 범위 내 Enemy Layer 탐색
            //Collider[] colliders = Physics.OverlapSphere(transform.position, towerInfo.attackRange, combinedLayerMask);

            //foreach (var collider in colliders)
            //{
            //    // 타겟의 Transform 추가
            //    tempTarget.Add(collider.transform);
            //}
            #endregion

            // 유효한 타겟 : 현재 체력이 0 이상이며, 타겟이 null이 아닌 경우 필터링
            tempTarget = tempTarget.Where(target => target != null && target.GetComponent<Health>().CurrentHealth > 0)
                .ToList();

            return tempTarget;
        }

        // 가장 가까운 타겟 설정
        void SetClosestTarget()
        {
            // 공격 범위 내 Enemy 받아오기
            targets = UpdateTargets();

            // 가장 가까운 타겟 찾기
            {
                float closestDistance = Mathf.Infinity;
                Transform closestTarget = null;

                foreach (var target in targets)
                {
                    // 타겟이 없거나 체력이 0이하로 떨어지면 다른 타겟을 찾도록
                    if (target == null || target.GetComponent<Health>().CurrentHealth <= 0) continue;

                    float distance = Vector3.Distance(transform.position, target.position);

                    // 타겟이 공격 범위 내에 있고, 가장 가까운 타겟인지 확인
                    if (distance <= towerInfo.attackRange && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = target;
                    }
                }
                currentTarget = closestTarget;
            }
        }

        // 발사
        protected virtual void Shoot()
        {
            // 슛 딜레이가 지났을 경우, 타겟의 체력이 0보다 큰 경우
            if (towerInfo.shootDelay < shootTime && currentTarget.GetComponent<Health>().CurrentHealth > 0)
            {
                // 발사체 생성
                GameObject projectilePrefab = Instantiate(towerInfo.projectile.prefab, firePoint.transform.position, Quaternion.identity);
                // Shoot Animation 재생
                if (animator != null)
                    animator.SetTrigger(Constants.ANIM_SHOOTTRIGGER);

                // 발사체 정보 초기화, 발사체의 가장 가까운 타겟설정, 공격 범위 내 타겟들 설정
                projectilePrefab.GetComponent<ProjectileBase>().Init(towerInfo.projectile, currentTarget);

                // 슛 타임 초기화
                shootTime = 0;
            }
        }

        // 타워 강화 (지속시간, 공격력, 방어력, 연사력, 공격사거리, 체력재생, 마나재생)
        // isPermanent 영구적인 버프인지 아닌지
        public void BuffTower(BuffContents buffContents, bool isPermanent)
        {
            // 버프 적용
            towerInfo.projectile.attack += buffContents.atk;
            towerInfo.armor += buffContents.armor;
            towerInfo.shootDelay /= buffContents.shootDelay;
            towerInfo.attackRange *= buffContents.atkRange;
            status.HealthRegenRatio *= buffContents.healthRegen;
            status.ManaRegenRatio *= buffContents.manaRegen;

            // 복구 Coroutine 실행
            if (isPermanent == false)
            {
                StartCoroutine(ResetTower(buffContents));
                // 효과 이펙트 적용
                ParticleSystem effect = Instantiate(buffEffect, gameObject.transform);
                Destroy(effect.gameObject, buffContents.duration);
            }
        }

        // 타워 복구 (지속시간, 공격력, 방어력, 연사력, 공격사거리, 체력재생, 마나재생)
        IEnumerator ResetTower(BuffContents buffContents)
        {
            yield return new WaitForSeconds(buffContents.duration);
            towerInfo.projectile.attack -= buffContents.atk;
            towerInfo.armor -= buffContents.armor;
            towerInfo.shootDelay *= buffContents.shootDelay;
            towerInfo.attackRange /= buffContents.atkRange;
            status.HealthRegenRatio /= buffContents.healthRegen;
            status.ManaRegenRatio /= buffContents.manaRegen;
        }

        // 공격방향 라인 랜더러
        private void DrawLine()
        {
            // 시작점 (오브젝트의 현재 위치)
            lineRenderer.SetPosition(0, transform.position);

            // 끝점 (오브젝트의 전방 방향으로 lineLength만큼 떨어진 위치)
            Vector3 endPosition = transform.position + transform.forward * gizmo.lineLength;
            lineRenderer.SetPosition(1, endPosition);
        }

        // 공격범위 기즈모
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmo.gizmoColor;
            Gizmos.DrawWireSphere(transform.position, gizmo.sphereRadius);
        }
    }
}
