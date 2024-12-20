using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    // "EnemyWay" Tag를 가진 오브젝트와 충돌 시 삭제
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 이름이 "Tree"인지 확인
        if (collision.gameObject.name == "Tree")
        {
            // "Tree" 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
