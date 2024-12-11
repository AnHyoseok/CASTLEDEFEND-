using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.Player
{
    public class ViewChange : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        //탑뷰
        private bool isViewChange = false;
        private Transform currentTransform;
        private Vector3 originalPosition; // 원래 위치
        private Quaternion originalRotation; // 원래 회전

        //숨길것들
        public GameObject[] hideObjects;
        //플레이어 더미
        public GameObject showObject;

        public InputActionProperty property;
        #endregion
        private void Start()
        {
            //참조
            playerState = GetComponent<PlayerState>();

            //초기화
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }
        private void Update()
        {
            if (property.action.WasCompletedThisFrame()|| Input.GetKeyDown(KeyCode.C))
            {
                ViewChangeGo();
            }
        }


        //탑뷰전환
        public void ViewChangeGo()
        {

            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange)
            {
                // 탑뷰에서 원래 위치로 돌아가기
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
                cinemachineBrain.enabled = false;
                Time.timeScale = 1f;
                //손,레이 가리고 더미 생성
                for (int i = 0; i < hideObjects.Length; i++)
                {
                    hideObjects[i].SetActive(true);
                }
                showObject.SetActive(false);
            }
            else
            {
                // 현재 위치 저장
                originalPosition = currentTransform.position;
                originalRotation = currentTransform.rotation;
                cinemachineBrain.enabled = true;

                //손,레이 가리고 더미 생성
                for (int i = 0; i < hideObjects.Length; i++)
                {
                    hideObjects[i].SetActive(false);
                }
                showObject.transform.position = originalPosition;
                Time.timeScale = 0f;
                //showObject.transform.position = originalPosition;
                showObject.SetActive(true);
            }

            isViewChange = !isViewChange;
        }
    }
}