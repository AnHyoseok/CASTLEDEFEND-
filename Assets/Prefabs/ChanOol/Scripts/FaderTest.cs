using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FaderTest : MonoBehaviour
{
    #region Variables
    private Image image; // Fader ÀÌ¹ÌÁö
    public AnimationCurve curve;
    #endregion

    private void Start()
    {
        // ÃÊ±âÈ­
        image = GetComponentInChildren<Image>();
    }

    // FadeOut (Á¡Á¡ ¾îµÎ¿öÁü)
    public void FadeTo()
    {
        StartCoroutine(FadeOut());
    }

    // FadeIn (Á¡Á¡ ¹à¾ÆÁü)
    public void FromFade()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        // 1ÃÊ µ¿¾È Image Alpha°ª 0 -> 1 (Á¡Á¡ ¾îµÎ¿öÁü)
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
        // 1ÃÊ µ¿¾È Image Alpha°ª 1 -> 0 (Á¡Á¡ ¹à¾ÆÁü)
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