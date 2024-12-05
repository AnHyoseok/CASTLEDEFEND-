using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRRaycastEndPoint : MonoBehaviour
{
    public GameObject buildAreaPrefab;  // 배치할 Build Area 프리팹
    public XRRayInteractor rayInteractor;  // XR 컨트롤러의 Ray Interactor
    private GameObject placedBuildArea;  // 생성된 Build Area의 참조
    private bool isBuildAreaActive = false;  // Build Area 활성 상태 캐싱

    void Start()
    {
        if (buildAreaPrefab != null)
        {
            // buildAreaPrefab을 생성
            placedBuildArea = Instantiate(buildAreaPrefab);

            // 생성된 buildAreaPrefab을 현재 오브젝트의 자식으로 설정
            placedBuildArea.transform.SetParent(transform);

            // 생성된 buildArea를 비활성화
            placedBuildArea.SetActive(false);
        }
    }

    void Update()
    {
        if (rayInteractor == null || placedBuildArea == null) return;
        
        RaycastHit hit;
        bool isBuildAreaActive = rayInteractor.TryGetCurrent3DRaycastHit(out hit);

        // Hit 된게 있으면 isBuildAreaActive true
        if (isBuildAreaActive == true)
        {
            placedBuildArea.SetActive(true);

            // Build Area 위치 업데이트
            placedBuildArea.transform.position = hit.point;

            // 표면의 기울기와 관계없이 x축 90도로 고정
            placedBuildArea.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // x축 90도 회전만 고정

            
        }
        else
        {
            isBuildAreaActive = false;
            placedBuildArea.SetActive(false);
        }
    }
}