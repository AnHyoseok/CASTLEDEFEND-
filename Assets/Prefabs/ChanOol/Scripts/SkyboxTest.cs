using System.Collections;
using UnityEngine;
using Defend.Enemy;

public class SkyboxTest : MonoBehaviour
{
    public Material blendedSkybox; // 낮 -> 밤
    //public Material blendedSkybox1; // 밤 -> 낮
    public float transitionSpeed = 1.0f; // 전환 속도
    private float blendValue = 0.0f; // Blend Factor

    public Light directionalLight; // 아침에 활성화 저녁에 비활성화
    
    public float lightTransitionSpeed = 1.0f; // 빛이 사라지는 속도
    // [SerializeField] GameObject Lamp; // 저녁에 활성화 아침에 비활성화
    private int stageWave = 0; // 현재 웨이브 번호
    //[SerializeField] private GameObject listSpawnManagerObject; // ListSpawnManager가 붙은 GameObject를 드래그로 연결
    //private ListSpawnManager listSpawnManager; // ListSpawnManager 스크립트의 waveCount 참조용
    private bool hasTransitioned = false; // 스카이박스 전환이 일어났는지 확인

    //맵에 있는 램프를 컨틀로 하기위한 변수
    private LightController light;


    void Start()
    {
        
        /*if (listSpawnManagerObject != null)
        {
            // 드래그로 연결한 GameObject에서 ListSpawnManager 컴포넌트 가져오기
            listSpawnManager = listSpawnManagerObject.GetComponent<ListSpawnManager>();

            if (listSpawnManager == null)
            {
                Debug.LogError("ListSpawnManager 컴포넌트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("ListSpawnManager가 붙은 GameObject를 설정하지 않았습니다!");
        }*/
        // Blended Skybox를 RenderSettings에 적용
        RenderSettings.skybox = blendedSkybox;

        // 스카이박스 Blend Factor 값 원래대로 (초기화)
        blendedSkybox.SetFloat("_Blend", 0.0f);

        //램프 컴포넌트를 찾아서 붙여준다
        light = FindAnyObjectByType<LightController>();

        // directionalLight.intensity 값 원래대로 (초기화)
        // directionalLight.intensity = 2f;

        //Debug.Log("TEST1 = " + blendValue);
        //Debug.Log("TEST2 = " + directionalLight.intensity);
    }

    void Update()
    {
        // ListSpawnManager 스크립트의 waveCount 변수를 stageWave에 대입
        //stageWave = listSpawnManager.waveCount;

        // 만약 현재 wave가 2와 같거나 높으면, 스카이박스 전환이 아직 일어나지 않았다면
        // 만약 남아있는 몬스터가 1마리 이상이고, 스카이박스 전환이 아직 일어나지 않았다면


        //if (Input.GetKeyDown(KeyCode.U) && hasTransitioned == false)

        if (ListSpawnManager.enemyAlive > 0 && hasTransitioned == false && ListSpawnManager.isSpawn == true)
        {
            StartCoroutine(TransitionToNight());
            hasTransitioned = true; // 스카이박스 전환 완료 체크
        }
        //RenderSettings.skybox.SetFloat("_Blend", blendValue);
        else if (ListSpawnManager.enemyAlive <= 0 && hasTransitioned == true && ListSpawnManager.isSpawn == false)
        {
            StartCoroutine(TransitionToDay());
            hasTransitioned = false; // 스카이박스 전환 해제
        }

        // 셰이더 코드에서 _Blend 값을 포함한 모든 전역 프로퍼티를 동기화
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetFloat("_Blend", blendValue);
    }

    IEnumerator TransitionToNight()
    {
        light.SetLampState(true);
        // 낮 -> 밤 (Blend 증가 및 빛 감소) 
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

            yield return null; // 매 프레임마다 업데이트
        }

        // 전환 완료 후 Blend 값 고정
        blendedSkybox.SetFloat("_Blend", 1.0f);

        // 전환 완료 후 intensity 값 고정
        directionalLight.intensity = 0f;
    }

    IEnumerator TransitionToDay()
    {
        light.SetLampState(false);
        // 밤 -> 낮 (Blend 감소 및 빛 증가) 
        while (blendValue > 0.0f || directionalLight.intensity < 1.0f)
        {
            if (blendValue > 0.0f)
            {
                blendValue -= Time.deltaTime * transitionSpeed;
                blendedSkybox.SetFloat("_Blend", Mathf.Clamp01(blendValue));
            }

            if (directionalLight.intensity < 1.0f)
            {
                directionalLight.intensity += Time.deltaTime * lightTransitionSpeed;
            }

            yield return null; // 매 프레임마다 업데이트
        }

        // 전환 완료 후 Blend 값 고정
        blendedSkybox.SetFloat("_Blend", 0.0f);

        // 전환 완료 후 intensity 값 고정
        directionalLight.intensity = 1.0f;
    }
}
