using Defend.TestScript;
using Defend.Utillity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Defend.Enemy
{
    /// <summary>
    /// Enemy의 움직임을 정의하는 클래스, WayPoint 및 종점 도착여부, Ratation 정의
    /// </summary>
    public class EnemyMoveController : MonoBehaviour
    {
        //필드
        #region Variable
        public float baseSpeed = 5f;
        [SerializeField] public float CurrentSpeed { get; private set; }   //이동 속도

        private float originSpeed;

        // 슬로우를 적용한 주체와 비율을 저장하는 딕셔너리
        private Dictionary<GameObject, float> moveSources = new Dictionary<GameObject, float>();

        private Health health;
        private EnemyController enemyController;

        private Transform target;   //이동할 목표지점        

        private int wayPointIndex = 0;  //wayPoints 배열을 관리하는 인덱스

        private bool isDeath = false;   //죽었는지 확인
        private bool isChanneling = false;

        //이동속도 변화를 감지할 UnityAction
        public UnityAction<float, float> MoveSpeedChanged;
        public UnityAction EnemyArrive;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            //참조
            //animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            enemyController = GetComponent<EnemyController>();

            //초기화
            CurrentSpeed = baseSpeed;

            //첫번째 목표지점 셋팅
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];


        }

        private void Start()
        {
            //Unity Action
            health.OnDie += OnDie;
            enemyController.OnChanneling += OnChanneling;
        }

        private void Update()
        {
            if (isDeath || isChanneling) return;
            Move();
        }

        //이동
        public void Move()
        {
            //이동 :방향(dir), Time.deltatiem, speed
            Vector3 dir = target.position - this.transform.position;
            // 객체를 목표 지점으로 회전        (Slerp를 이용해서 천천히 회전)
            if (dir != Vector3.zero) // Zero 방향 확인 (안전 처리)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * CurrentSpeed) * 2);
            }
            //transform.LookAt(target);         (LookAt을 이용해서 바로 회전)

            //이동
            transform.Translate(dir.normalized * Time.deltaTime * CurrentSpeed, Space.World);

            //도착판정
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 0.2f)
            {
                SetNextTarget();
            }
        }

        //다음 목표 지점 셋팅
        void SetNextTarget()
        {
            if (wayPointIndex == WayPoints.points.Length - 1)
            {
                Arrive();
                return;
            }

            wayPointIndex++;
            target = WayPoints.points[wayPointIndex];
        }

        public void ChangedMoveSpeed(GameObject source, float rate)
        {
            if (moveSources.ContainsKey(source))
            {
                // 이미 동일한 주체가 적용한 경우, 증감 비율을 갱신
                moveSources[source] = rate;
            }
            else
            {
                // 새로운 주체라면 추가
                moveSources.Add(source, rate);
            }

            // 최종 속도 계산
            UpdateCurrentSpeed();
        }

        public void RemoveMoveSource(GameObject source)
        {
            if (moveSources.ContainsKey(source))
            {
                // 증감 주체 제거
                moveSources.Remove(source);

                // 최종 속도 계산
                UpdateCurrentSpeed();
            }
        }

        private void UpdateCurrentSpeed()
        {
            float totalRate = 0;

            // 모든 슬로우 비율을 합산
            foreach (var rate in moveSources.Values)
            {
                if (enemyController.type == EnemyType.Boss)
                {
                    if (rate > 0)
                    {
                        totalRate += rate;
                    }
                    Debug.Log($"{enemyController.type}");
                }
                else
                {
                    totalRate += rate;
                }
            }

            //증감효과는 슬로우 = 66%, 증가 = 50%로 제한
            totalRate = Mathf.Clamp(totalRate, -0.66f, 0.5f);

            // 이동 속도 갱신
            CurrentSpeed = baseSpeed * (1.0f + totalRate);

            originSpeed = CurrentSpeed;

            //이동속도는 baseSpeed를 기반으로 +-50%를 초과할 수 없음
            //CurrentSpeed = Mathf.Clamp(CurrentSpeed, (baseSpeed / 2), (baseSpeed * 2));

            //Debug.Log("최종 속도 = " + CurrentSpeed);

            // 속도 변경 이벤트 호출
            MoveSpeedChanged?.Invoke(CurrentSpeed, totalRate);
        }

        //private void UpdateCurrentSpeed()
        //{
        //    float currentSpeed = baseSpeed;

        //    //복리 계산: 현재 속도를 기준으로 슬로우 효과 적용
        //    foreach (var rate in moveSources.Values)
        //    {
        //        currentSpeed -= currentSpeed * -rate; // 복리 적용
        //    }

        //    CurrentSpeed = currentSpeed; // 최종 속도 업데이트

        //    //    //이동속도는 baseSpeed를 기반으로 +-50%를 초과할 수 없음
        //    CurrentSpeed = Mathf.Clamp(currentSpeed, (baseSpeed / 2), (baseSpeed * 2));

        //    Debug.Log("최종 속도 = " + CurrentSpeed);

        //    // 속도 변경 이벤트 호출
        //    MoveSpeedChanged?.Invoke(CurrentSpeed);
        //}

        public IEnumerator SetZeroSpeed()
        {
            //originSpeed = CurrentSpeed;
            CurrentSpeed = 0.0f;
            yield return new WaitForSeconds(5f);
            CurrentSpeed = originSpeed;
        }


        //목표지점 도착 처리
        void Arrive()
        {
            GetComponent<Animator>().SetBool(Constants.ENEMY_ANIM_ISARRIVE, true);
            EnemyArrive?.Invoke();
            // Update를 멈추기 위해 컴포넌트 비활성화
            enabled = false;
        }

        private void OnDie()
        {
            isDeath = true;
        }

        private void OnChanneling()
        {
            Debug.Log("호출됐다! = " + isChanneling);
            isChanneling = !isChanneling;
        }
    }
}