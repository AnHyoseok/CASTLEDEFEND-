using Defend.Utillity;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] List<Transform> targets;           // 바라볼 타겟 오브젝트
        public Transform currentTarget;                     // 현재 가장 가까운 타겟
        public List<LayerMask> targetLayerList;             // 타겟 오브젝트의 레이어

        // 타워 정보
        [SerializeField] protected TowerInfo towerInfo;
        #endregion

        #region Variables For Test
        public Color gizmoColor = Color.green;              // 기즈모 색상
        public float sphereRadius;                          // 구의 반지름
        public float lineLength = 10f;                      // 라인의 길이

        LineRenderer lineRenderer;                          // 라인 랜더러
        #endregion

        protected virtual void Start()
        {
            // Layer 설정
            targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_ENEMY));
            targetLayerList.Add(LayerMask.GetMask(Constants.LAYER_BOSS));

            // 일정 주기로 타겟 탐색
            InvokeRepeating(nameof(SetClosestTarget), 0f, towerInfo.detectDelay);

            #region Test를 위한 시각화 => LineRenderer 초기화, Gizmo
            {
                // 기즈모 범위 초기화
                sphereRadius = towerInfo.attackRange;

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

            // TEST
            DrawLine();            // 타겟 방향으로 라인 그리기
        }

        // 타겟의 방향으로 회전
        protected virtual void SetRotationToTarget()
        {
            if (currentTarget != null)
            {
                // 타겟 설정
                Vector3 targetPosition = currentTarget.position;

                // 현재 오브젝트에서 타겟을 향하는 방향 계산
                Vector3 direction = targetPosition - transform.position;

                // 타겟을 바라보는 회전 계산
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 서서히 회전 (Slerp 사용)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, towerInfo.rotationSpeed * Time.deltaTime);
            }
        }

        // 범위 내의 타겟을 갱신
        List<Transform> UpdateTargets()
        {
            // 기존 타겟 초기화
            targets.Clear();

            // 반환할 타겟들
            List<Transform> tempTarget = new List<Transform>();

            // targetLayerList에서 여러 레이어를 비트 연산으로 결합
            int combinedLayerMask = 0;
            foreach (var layerMask in targetLayerList)
            {
                combinedLayerMask |= layerMask.value;  // 비트 연산으로 레이어 결합
            }

            // 공격 범위 내 Enemy Layer 탐색
            Collider[] colliders = Physics.OverlapSphere(transform.position, towerInfo.attackRange, combinedLayerMask);

            foreach (var collider in colliders)
            {
                // 타겟의 Transform 추가
                tempTarget.Add(collider.transform);
            }

            return tempTarget;
        }

        // 가장 가까운 타겟 설정
        void SetClosestTarget()
        {
            // Enemy 받아오기
            targets = UpdateTargets();

            // 가장 가까운 타겟 찾기
            {
                float closestDistance = Mathf.Infinity;
                Transform closestTarget = null;

                foreach (var target in targets)
                {
                    if (target == null) continue;

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

        // 공격방향 라인 랜더러
        private void DrawLine()
        {
            // 시작점 (오브젝트의 현재 위치)
            lineRenderer.SetPosition(0, transform.position);

            // 끝점 (오브젝트의 전방 방향으로 lineLength만큼 떨어진 위치)
            Vector3 endPosition = transform.position + transform.forward * lineLength;
            lineRenderer.SetPosition(1, endPosition);
        }

        // 공격범위 기즈모
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }
    }
}
