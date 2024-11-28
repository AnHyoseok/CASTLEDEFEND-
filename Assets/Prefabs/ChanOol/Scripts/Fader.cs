using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // Fade Speed 값

    [SerializeField] private AnimationCurve fadeCurve;  // 페이드 효과가 적용되는 알파 값을 곡선의 값으로 설정
    public Image image;                                 // Fader 이미지 (검은바탕 이미지)
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 초기화
        image = GetComponentInChildren<Image>();

        // 실행
        StartCoroutine(FadeOutIn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fade Out/In 효과 한세트 함수
    private IEnumerator FadeOutIn()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1));    // FadeOut

            yield return StartCoroutine(Fade(1, 0));    // FadeIn

            break;
        }
    }

    // Fader(1, 0) : FadeIn
    // Fader(0, 1) : FadeOut

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime으로 나누어서 fadeTime 시간동안
            // percent의 값이 0에서 1로 증가
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파값을 Start부터 End까지 fadeTime 시간동안 변화
            Color color = image.color;
            // color.a = Mathf.Lerp(start, end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
