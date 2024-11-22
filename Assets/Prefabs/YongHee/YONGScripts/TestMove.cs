using UnityEngine;

public class TestMove : MonoBehaviour
{
    #region Variables
    public float speed = 2f; // 이동 속도
    public Transform[] wayPoints;
    private int currentWaypointIndex = 0; // 현재 이동 중인 웨이포인트 인덱스
    private Vector3 targetPosition; // 현재 목표 위치
    #endregion
    void Start()
    {
        if (wayPoints.Length > 0)
        {
            // 첫 번째 웨이포인트를 목표 위치로 설정
            targetPosition = wayPoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        if (wayPoints.Length == 0)
            return; // 웨이포인트가 없으면 업데이트 중단

        // 오브젝트를 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 다음 웨이포인트로 이동 (마지막 웨이포인트를 넘어가면 첫 번째 웨이포인트로 순환)
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
            targetPosition = wayPoints[currentWaypointIndex].position;
        }
    }
}
