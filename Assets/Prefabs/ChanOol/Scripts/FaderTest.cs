using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FaderTest : MonoBehaviour
{
    #region Variables
    private Image image; // Fader 이미지
    public AnimationCurve curve;
    #endregion

    private void Start()
    {
        // 초기화
        image = GetComponentInChildren<Image>();
    }



    // FadeOut
    public void FadeTo()
    {
        // FadeOut 시작 전 알파 초기화
        // image.color = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(FadeOut());
    }

    // FadeIn
    public void FromFade()
    {
        // FadeIn 시작 전 알파 초기화
        // image.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        // 1초 동안 image alpha 0 -> 1
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        // 1초 동안 image alpha 1 -> 0
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }
}