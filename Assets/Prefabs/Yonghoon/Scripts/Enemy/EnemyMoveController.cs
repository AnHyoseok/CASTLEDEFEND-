using UnityEngine;

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
        [SerializeField] private float speed;   //이동 속도

        //Arrive상태로 변경할 animator
        //private Animator animator;

        private Transform target;   //이동할 목표지점        

        private int wayPointIndex = 0;  //wayPoints 배열을 관리하는 인덱스
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //참조
            //animator = GetComponent<Animator>();

            //초기화
            speed = baseSpeed;

            //첫번째 목표지점 셋팅
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];
        }

        private void Update()
        {
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
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * speed) * 2);
            }
            //transform.LookAt(target);         (LookAt을 이용해서 바로 회전)

            //이동
            transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);

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

        //목표지점 도착 처리
        void Arrive()
        {
            GetComponent<Animator>().SetBool("IsArrive", true);
            GetComponent<EnemyAttackController>().enabled = true;

            // Update를 멈추기 위해 컴포넌트 비활성화
            enabled = false;
        }

        //슬로우 효과 적용시
        public void Slow(float rate)
        {
            speed = baseSpeed * (1.0f - rate);
        }

        //가속화 효과 적용시
        public void Fast(float rate)
        {
            speed = baseSpeed * (1.0f + rate);
        }
    }
}