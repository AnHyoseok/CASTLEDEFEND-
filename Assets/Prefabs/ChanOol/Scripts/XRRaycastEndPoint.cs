using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRRaycastEndPoint : MonoBehaviour
{
    public XRRayInteractor rayInteractor;  // XR 컨트롤러의 Ray Interactor
    private GameObject buildArea;  // 자식 오브젝트 Build Area 참조용
    //private bool isBuildAreaActive = false;  // Build Area 활성 상태 캐싱
    private Renderer buildAreaRenderer; // Build Area의 Renderer 참조

    void Start()
    {

        // 자식 오브젝트를 이름으로 찾아서 참조
        buildArea = transform.Find("BuildArea")?.gameObject;

    }

    void Update()
    {
        if (rayInteractor == null || buildArea == null) return;

        RaycastHit hit;
        bool isBuildAreaActive = rayInteractor.TryGetCurrent3DRaycastHit(out hit);

        // Hit 된게 있으면 isBuildAreaActive true
        if (isBuildAreaActive == true)
        {
            buildArea.SetActive(true);

            // Build Area 위치 업데이트
            buildArea.transform.position = hit.point;

            // 표면의 기울기와 관계없이 x축 90도로 고정
            buildArea.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // x축 90도 회전만 고정
        }
        else
        {
            isBuildAreaActive = false;
            buildArea.SetActive(false);
        }
    }
}