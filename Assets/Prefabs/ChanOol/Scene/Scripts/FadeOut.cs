using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    #region Variables
    public Image image; // Fader 이미지 넣기
    #endregion

    private void Start()
    {
        // 초기화
        // Fader의 자식 오브젝트인 FaderImage를 가져오기
        image = GetComponentInChildren<Image>();

        // 가져온 FaderImage 이미지 r,g,b,a 값 초기화
        image.color = new Color(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        // image.color
        Color color = image.color;

        // 알파 값(a)이 1보다 작으면 알파 값 증가
        if (color.a < 1f)
        {
            // Time.deltaTime 만큼 감소하기때문에 1에서 0까지 1초동안 감소
            color.a += Time.deltaTime;
        }

        //바뀐 색상 정보를 image.color에 저장
        image.color = color;
    }

}

