using UnityEngine;

public class BuildAreaChecker : MonoBehaviour
{
    public Material shadow; // 생성 가능 표시 마테리얼
    public Material shadowRed; // 생성 불가 표시 마테리얼
    [SerializeField] private bool isBuild; // 생성 가능이면 true 불가하면 false
    private Renderer objRenderer; // 현재 이 오브젝트의 렌더러

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 타워를 생성할수있으면
        if (isBuild == true)
        {
            CanBuildArea(); // 검은색 마테리얼
        }
        // 타워를 생성할수없으면
        else if (isBuild == false)
        {
            CannotBuildArea(); // 빨간색 마테리얼
        }
    }

    public void CanBuildArea()
    {
        objRenderer.material = shadow; // 검은색 마테리얼
    }

    public void CannotBuildArea()
    {
        objRenderer.material = shadowRed; // 빨간색 마테리얼
    }
}
