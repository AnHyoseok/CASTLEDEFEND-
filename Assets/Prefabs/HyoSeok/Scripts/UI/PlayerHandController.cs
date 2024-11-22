using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //테스트용 시계
        public TextMeshProUGUI timeText;
        private float time;

        //탑뷰
        private bool isViewChange = false;
        public Transform currentTransform;
        private Vector3 originalPosition; // 원래 위치
        private Quaternion originalRotation; // 원래 회전
        #endregion

        private void Start()
        {
            //초기화
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }

        private void Update()
        {
            time += Time.deltaTime;
            timeText.text = time.ToString("F2");
        }

        public void ViewChange()
        {
            Debug.Log(isViewChange);
            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange==true)
            {
                cinemachineBrain.enabled = isViewChange;
            }
            else
            {
                // 원래 위치로 돌아가기
                cinemachineBrain.enabled = isViewChange;
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
            }

            isViewChange = !isViewChange;
        }
    }
}