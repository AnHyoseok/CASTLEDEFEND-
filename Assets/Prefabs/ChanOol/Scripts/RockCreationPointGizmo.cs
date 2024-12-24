using UnityEngine;

public class RockCreationPointGizmo : MonoBehaviour
{
    // Scene 뷰에서 테두리를 그리는 메서드
    private void OnDrawGizmos()
    {
        // 현재 위치를 기준으로 테두리를 계산합니다.
        Vector3 center = transform.position;
        Vector3 size = new Vector3(2, 0, 2);

        // 기즈모 색상 설정
        Gizmos.color = new Color(0.9f, 0.9f, 0.9f); // 밝은 회색

        // 테두리 박스를 그립니다.
        Gizmos.DrawWireCube(center, size);
    }
}
