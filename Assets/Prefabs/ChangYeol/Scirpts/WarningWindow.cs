using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class WarningWindow : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI WarningText;
        public Button confirmButton;
        public Button cancelButton;
        public GameObject WarningUI;
        #endregion
        /*경고창 보여주는 함수 (보여줄 텍스트, 확인 버튼 클릭 시 실행될 동작, 취소 버튼 클릭 시 실행될 동작,
        확인 버튼 클릭 시 실행될 동작 과 취소 버튼 클릭 시 실행될 동작이 없으면 UI 보여주는 시간)*/
        public void ShowWarning(string message, System.Action onConfirm = null, System.Action onCancel = null,float show = 3)
        {
            WarningText.text = message;
            confirmButton.onClick.AddListener(() =>
            {
                // 확인 버튼 클릭 시 실행될 동작
                onConfirm?.Invoke();
                Hide();
            });
            cancelButton.onClick.AddListener(() =>
            {
                // 취소 버튼 클릭 시 실행될 동작
                onCancel?.Invoke();
                Hide();
            });

            cancelButton.gameObject.SetActive(onCancel != null);
            confirmButton.gameObject.SetActive(onConfirm != null);
            if(onCancel == null && onConfirm ==null)
            {
                StartCoroutine(ShowWindow(show));
                return;
            }
            WarningUI.SetActive(true);
            // 여기에 경고창 나타날 때 시각 효과 추가 (예: 애니메이션)
        }

        public void Hide()
        {
            WarningUI.SetActive(false);
            // 여기에 경고창 사라질 때 시각 효과 추가 (예: 애니메이션)
        }
        IEnumerator ShowWindow(float showtime)
        {
            WarningUI.SetActive(true);
            yield return new WaitForSeconds(showtime);
            WarningUI.SetActive(false);
        }
    }
}