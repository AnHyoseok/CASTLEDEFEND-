using UnityEngine;

namespace Defend.Enemy
{
    public class EnemyMoveController : MonoBehaviour
    {
        //필드
        #region Variable
        [SerializeField] private float speed;   //이동 속도
        [SerializeField] private float startSpeed = 3f; //이동 시작 속도

        private Transform target;   //이동할 목표지점        

        private int wayPointIndex = 0;  //wayPoints 배열을 관리하는 인덱스

        //보스인지 확인
        private bool isBoss;

        private float countdown;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //초기화
            speed = startSpeed;
            countdown = 3.1f;
            isBoss = gameObject.name.Contains("Dragon");

            //첫번째 목표지점 셋팅
            wayPointIndex = 0;
            target = WayPoints.points[wayPointIndex];
        }

        private void Update()
        {
            if (isBoss)
            {
                //Debug.Log(countdown);
                if (countdown <= 0)
                {
                    Move();
                }
                else
                {
                    countdown -= Time.deltaTime;
                }
            }
            else
            {
                Move();
            }
            //속도 복원
            //speed = startSpeed;
        }

        //이동
        public void Move()
        {
            //이동 :방향(dir), Time.deltatiem, speed
            Vector3 dir = target.position - this.transform.position;
            //// 객체를 목표 지점으로 회전
            if (dir != Vector3.zero) // Zero 방향 확인 (안전 처리)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * speed) * 2);
            }
            //transform.LookAt(target);

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
            //도착처리 (ex 라이프 감소 등)

            //게임 오브젝트 kill
            Debug.Log("Enemy Arrive!");
        }

        //슬로우 효과 적용시
        public void Slow(float rate)
        {
            speed = startSpeed * (1.0f - rate);
        }
    }
}