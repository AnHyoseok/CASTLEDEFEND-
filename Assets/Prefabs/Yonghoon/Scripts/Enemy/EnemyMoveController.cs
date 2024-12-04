using Defend.TestScript;
using System;
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
        [SerializeField] private float baseSpeed = 5f;
        [SerializeField] public float CurrentSpeed { get; private set; }   //이동 속도

        // 슬로우를 적용한 주체와 비율을 저장하는 딕셔너리
        private Dictionary<GameObject, float> moveSources = new Dictionary<GameObject, float>();

        ////테스트용
        //GameObject enemy1 = new GameObject("Enemy1");
        //GameObject enemy2 = new GameObject("Enemy2");
        //GameObject enemy3 = new GameObject("Enemy3");

        private Health health;
        //Arrive상태로 변경할 animator
        //private Animator animator;

        private Transform target;   //이동할 목표지점        

        private int wayPointIndex = 0;  //wayPoints 배열을 관리하는 인덱스

        private bool isDeath;

        //이동속도 변화를 감지할 UnityAction
        public UnityAction<float> MoveSpeedChanged;
        public UnityAction EnemyArrive;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            //참조
            //animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //초기화
            CurrentSpeed = baseSpeed;
            isDeath = false;

            //첫번째 목표지점 셋팅
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];

            //Unity Action
            health.OnDie += OnDie;
        }

        private void Update()
        {
            if (isDeath) return;
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
                // 이미 동일한 주체가 적용한 경우, 슬로우 비율을 갱신
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
                // 슬로우 주체 제거
                moveSources.Remove(source);

                // 최종 속도 계산
                UpdateCurrentSpeed();
            }
        }

        private void UpdateCurrentSpeed()
        {
            float currentSpeed = baseSpeed;

            // 복리 계산: 현재 속도를 기준으로 슬로우 효과 적용
            foreach (var rate in moveSources.Values)
            {
                currentSpeed -= currentSpeed * -rate; // 복리 적용
            }

            CurrentSpeed = currentSpeed; // 최종 속도 업데이트

            Debug.Log("최종 속도 = " + CurrentSpeed);

            // 속도 변경 이벤트 호출
            MoveSpeedChanged?.Invoke(CurrentSpeed);
        }

        //이동속도 변경시
        //public void ChangedMoveSpeed(float rate)
        //{


        //    float checkChangeSpeed = baseSpeed - (baseSpeed * (1.0f + rate));
        //    Debug.Log("바뀌는 값 = " + checkChangeSpeed);
        //    CurrentSpeed -= checkChangeSpeed;
        //    Debug.Log("현재 이속 = " + CurrentSpeed);

        //    MoveSpeedChanged?.Invoke(CurrentSpeed);
        //}

        //목표지점 도착 처리
        void Arrive()
        {
            GetComponent<Animator>().SetBool("IsArrive", true);
            EnemyArrive?.Invoke();
            // Update를 멈추기 위해 컴포넌트 비활성화
            enabled = false;
        }

        private void OnDie()
        {
            isDeath = true;
        }
    }
}