using UnityEngine;

namespace Defend.Enemy
{
    public class EnemyMoveController : MonoBehaviour
    {
        //�ʵ�
        #region Variable
        [SerializeField] private float speed;   //�̵� �ӵ�
        [SerializeField] private float startSpeed = 3f; //�̵� ���� �ӵ�

        private Transform target;   //�̵��� ��ǥ����        

        private int wayPointIndex = 0;  //wayPoints �迭�� �����ϴ� �ε���

        //�������� Ȯ��
        private bool isBoss;

        private float countdown;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //�ʱ�ȭ
            speed = startSpeed;
            countdown = 3.1f;
            isBoss = gameObject.name.Contains("Dragon");

            //ù��° ��ǥ���� ����
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
            //�ӵ� ����
            //speed = startSpeed;
        }

        //�̵�
        public void Move()
        {
            //�̵� :����(dir), Time.deltatiem, speed
            Vector3 dir = target.position - this.transform.position;
            //// ��ü�� ��ǥ �������� ȸ��
            if (dir != Vector3.zero) // Zero ���� Ȯ�� (���� ó��)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (Time.deltaTime * speed) * 2);
            }
            //transform.LookAt(target);

            //�̵�
            transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);

            //��������
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 0.2f)
            {
                SetNextTarget();
            }
        }

        //���� ��ǥ ���� ����
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

        //��ǥ���� ���� ó��
        void Arrive()
        {
            //����ó�� (ex ������ ���� ��)

            //���� ������Ʈ kill
            Debug.Log("Enemy Arrive!");
        }

        //���ο� ȿ�� �����
        public void Slow(float rate)
        {
            speed = startSpeed * (1.0f - rate);
        }
    }
}