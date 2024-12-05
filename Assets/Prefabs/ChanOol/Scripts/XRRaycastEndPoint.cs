using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRRaycastEndPoint : MonoBehaviour
{
    public GameObject buildAreaPrefab;  // ��ġ�� Build Area ������
    public XRRayInteractor rayInteractor;  // XR ��Ʈ�ѷ��� Ray Interactor
    private GameObject placedBuildArea;  // ������ Build Area�� ����
    private bool isBuildAreaActive = false;  // Build Area Ȱ�� ���� ĳ��

    void Start()
    {
        if (buildAreaPrefab != null)
        {
            // buildAreaPrefab�� ����
            placedBuildArea = Instantiate(buildAreaPrefab);

            // ������ buildAreaPrefab�� ���� ������Ʈ�� �ڽ����� ����
            placedBuildArea.transform.SetParent(transform);

            // ������ buildArea�� ��Ȱ��ȭ
            placedBuildArea.SetActive(false);
        }
    }

    void Update()
    {
        if (rayInteractor == null || placedBuildArea == null) return;
        
        RaycastHit hit;
        bool isBuildAreaActive = rayInteractor.TryGetCurrent3DRaycastHit(out hit);

        // Hit �Ȱ� ������ isBuildAreaActive true
        if (isBuildAreaActive == true)
        {
            placedBuildArea.SetActive(true);

            // Build Area ��ġ ������Ʈ
            placedBuildArea.transform.position = hit.point;

            // ǥ���� ����� ������� x�� 90���� ����
            placedBuildArea.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // x�� 90�� ȸ���� ����

            
        }
        else
        {
            isBuildAreaActive = false;
            placedBuildArea.SetActive(false);
        }
    }
}