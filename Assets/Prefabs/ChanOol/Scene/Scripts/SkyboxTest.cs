using System.Collections;
using UnityEngine;
using Defend.Enemy;

public class SkyboxTest : MonoBehaviour
{
    public Material blendedSkybox; // Blended Skybox Material
    public float transitionSpeed = 1.0f; // ��ȯ �ӵ�
    private float blendValue = 0.0f; // Blend Factor

    public Light directionalLight; // ��ħ�� Ȱ��ȭ ���ῡ ��Ȱ��ȭ
    
    public float lightTransitionSpeed = 1.0f; // ���� ������� �ӵ�
    // [SerializeField] GameObject Lamp; // ���ῡ Ȱ��ȭ ��ħ�� ��Ȱ��ȭ
    private int stageWave = 0; // ���� ���̺� ��ȣ
    [SerializeField] private GameObject listSpawnManagerObject; // ListSpawnManager�� ���� GameObject�� �巡�׷� ����
    private ListSpawnManager listSpawnManager; // ListSpawnManager ��ũ��Ʈ�� waveCount ������
    private bool hasTransitioned = false; // ��ī�̹ڽ� ��ȯ�� �Ͼ���� Ȯ��

    
    void Start()
    {
        
        if (listSpawnManagerObject != null)
        {
            // �巡�׷� ������ GameObject���� ListSpawnManager ������Ʈ ��������
            listSpawnManager = listSpawnManagerObject.GetComponent<ListSpawnManager>();

            if (listSpawnManager == null)
            {
                Debug.LogError("ListSpawnManager ������Ʈ�� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("ListSpawnManager�� ���� GameObject�� �������� �ʾҽ��ϴ�!");
        }
        // Blended Skybox�� RenderSettings�� ����
        RenderSettings.skybox = blendedSkybox;

        // ��ī�̹ڽ� Blend Factor �� ������� (�ʱ�ȭ)
        blendedSkybox.SetFloat("_Blend", 0.0f);

        // directionalLight.intensity �� ������� (�ʱ�ȭ)
        // directionalLight.intensity = 2f;
    }

    void Update()
    {
        // ListSpawnManager ��ũ��Ʈ�� waveCount ������ stageWave�� ����
        stageWave = listSpawnManager.waveCount;

        // ���� ���� wave�� 2�� ���ų� ������, ��ī�̹ڽ� ��ȯ�� ���� �Ͼ�� �ʾҴٸ�
        if (stageWave >= 2 && hasTransitioned == false)
        {
            StartCoroutine(TransitionToNight());
            hasTransitioned = true; // ��ī�̹ڽ� ��ȯ �Ϸ� üũ
        }
    }

    IEnumerator TransitionToNight()
    {
        // �� -> �� (Blend ���� �� �� ����) 
        while (blendValue < 1.0f || directionalLight.intensity > 0)
        {
            if (blendValue < 1.0f)
            {
                blendValue += Time.deltaTime * transitionSpeed;
                blendedSkybox.SetFloat("_Blend", Mathf.Clamp01(blendValue));
            }

            if (directionalLight.intensity > 0)
            {
                directionalLight.intensity -= Time.deltaTime * lightTransitionSpeed;
            }

            yield return null; // �� �����Ӹ��� ������Ʈ
        }

        // ��ȯ �Ϸ� �� Blend �� ����
        blendedSkybox.SetFloat("_Blend", 1.0f);

        // ��ȯ �Ϸ� �� intensity �� ����
        directionalLight.intensity = 0f;
    }
}
