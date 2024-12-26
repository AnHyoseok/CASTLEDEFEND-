using System.Collections;
using UnityEngine;

public class SkyboxTransitionTest : MonoBehaviour
{
    public Material skybox1; // 첫 번째 스카이박스
    public Material skybox2; // 두 번째 스카이박스
    public float transitionSpeed = 1.0f; // 전환 속도
    public Material blendedSkybox;

    private float lerpValue = 0.0f;
    private bool isTransitioning = false;

    void Start()
    {
        // Blended Material을 RenderSettings.skybox로 설정
        RenderSettings.skybox = blendedSkybox;
        StartCoroutine(enumerator());
    }

    void Update()
    {
        if (isTransitioning)
        {
            // Lerp 진행
            lerpValue += Time.deltaTime * transitionSpeed;
            blendedSkybox.SetFloat("_Blend", lerpValue);

            // 완료되면 종료
            if (lerpValue >= 1.0f)
            {
                isTransitioning = false;
                lerpValue = 0.0f;

                // 최종 스카이박스 적용
                RenderSettings.skybox = skybox2;
            }
        }
    }

    // 전환 시작
    public void StartTransition()
    {
        isTransitioning = true;
        RenderSettings.skybox = blendedSkybox; // Blended Material 설정
        blendedSkybox.SetTexture("_Sky1", skybox1.GetTexture("_MainTex"));
        blendedSkybox.SetTexture("_Sky2", skybox2.GetTexture("_MainTex"));
    }

    IEnumerator enumerator()
    {
        //Debug.Log("Enumerator started");
        yield return new WaitForSeconds(3f);

        //Debug.Log("Transition started");
        StartTransition();
    }
}

