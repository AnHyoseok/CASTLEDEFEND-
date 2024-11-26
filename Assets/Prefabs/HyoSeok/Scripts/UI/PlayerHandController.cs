using Defend.Player;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        //테스트용 시계
        public TextMeshProUGUI timeText;
        public GameObject viewButton;
        public GameObject uiOnButton;
        public GameObject viewCanvas;

        //자원개수
        public TextMeshProUGUI rockheld;
        public TextMeshProUGUI treeheld;
        public TextMeshProUGUI moneyheld;

        private float time;

        //메뉴 선택 버튼
        public GameObject menuCanvas;
        public GameObject statusButton;
        public GameObject potalButton;
        // 현재 활성화된 메뉴 상태
        private bool isViewMenuActive = true; 



        //포탈 캔버스
        public GameObject potalCanvas;        
        //탑뷰
        private bool isViewChange = false;
        private Transform currentTransform;
        private Vector3 originalPosition; // 원래 위치
        private Quaternion originalRotation; // 원래 회전

        //버튼 켜지는여부
        private bool isOnUi;
        #endregion

        private void Start()
        {
            playerState = GetComponent<PlayerState>();
            //초기화
            currentTransform = gameObject.transform;
            originalPosition = currentTransform.position;
            originalRotation = currentTransform.rotation;
        }

        private void Update()
        {

            time += Time.deltaTime;
            timeText.text = time.ToString("F2");
            rockheld.text = playerState.rock.ToString();
            treeheld.text = playerState.tree.ToString();
            moneyheld.text = playerState.money.ToString() + "G";

        }

        public void ViewChange()
        {
           
            CinemachineBrain cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
            if (isViewChange)
            {
                // 탑뷰에서 원래 위치로 돌아가기
                currentTransform.position = originalPosition;
                currentTransform.rotation = originalRotation;
                cinemachineBrain.enabled = false; 
            }
            else
            {
                // 현재 위치 저장
                originalPosition = currentTransform.position;
                originalRotation = currentTransform.rotation;
                cinemachineBrain.enabled = true;
            }

            isViewChange = !isViewChange;
        }


        //ui 켜기
        public void ShowButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
                menuCanvas.SetActive(true);
            }
            else
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
                menuCanvas.SetActive(true);
            }
        }

        //ui 끄기
        public void hideButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
                menuCanvas.SetActive(true);
            }
            else
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
                menuCanvas.SetActive(false);
            }
        }

       
        public void ChangeMenu()
        {
            if (isViewMenuActive)
            {
                viewCanvas.SetActive(false);
                potalCanvas.SetActive(true);
                isViewMenuActive = false;
            }
            else
            {
                potalCanvas.SetActive(false);
                viewCanvas.SetActive(true);
                isViewMenuActive = true; 
            }
        }

    }
}