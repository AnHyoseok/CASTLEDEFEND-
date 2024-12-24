using UnityEngine;

namespace Defend.Enemy
{
    //Waypoint들을 관리하는 클래스
    public class WayPoints : MonoBehaviour
    {
        //필드
        public static Transform[] points;
        // Inspector에 보여질 인스턴스 변수
        [SerializeField] private Transform[] serializedTransforms;
        private void Awake()
        {
            points = new Transform[this.transform.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = this.transform.GetChild(i);
            }
            serializedTransforms = points;
        }

    }
}