using Defend.Enemy;
using Defend.TestScript;
using Defend.Tower;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
/// <summary>
/// Tutorial Scene을 관리하는 Manager
/// </summary>
namespace Defend.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        #region Variables

        #region UI
        public Canvas tutorialCanvas;
        public GameObject backgroundUI;
        public TextMeshProUGUI guideText;
        public Button hideButton;
        public Button showButton;
        public GameObject endTutorialUI;
        public Button nextButton;
        public Button retryButton;
        public GameObject buildUI;
        public TMP_SpriteAsset axeSpriteAsset;
        public TMP_SpriteAsset pickaxSpriteAsset;
        public TMP_SpriteAsset handSpriteAsset;
        #endregion

        public GameObject player;               // Player
        public GameObject playerDummy;          // PlayerDummy TopView 인식용 Obj
        public GameObject rock;                 // 튜토리얼용 rock
        public GameObject tree;                 // 튜토리얼용 tree
        public GameObject castle;               // 튜토리얼용 castle
        public GameObject axe;                  // 플레이어 Axe
        public GameObject PickAxe;              // 플레이어 PickAxe
        public ListSpawnManager lsm;            // ListSpawnManager
        public float fontSize;                  // guideText font size
        public InputActionProperty leftXButton; // LeftHandController 'X'
        public InputActionProperty leftYButton; // LeftHandController 'Y'
        public InputActionProperty leftMenuButton; // LeftHandController 'Menu'
        private string guideString;             // UI에 나타나는 문구
        private UnityAction buttonAnimAction;   // ShowButton Animation UnityAction
        CinemachineBrain cmb;                   // Player의 CinemachineBrain
        [SerializeField] private string isNewGuide = "IsNewGuide"; // Animation bool 변수
        [SerializeField] private string loadToScene; // 종료 후 로드 할 Scene

        #region Step 진행 Bool Variables
        // TopVIew 확인하기
        private bool isA = true;
        // 곡괭이로 바꾸기
        private bool isB = false;
        // 채광하기
        private bool isC = false;
        // 벌목하기
        private bool isD = false;
        // User UI 띄우기
        private bool isE = false;
        // 라운드 시작하기
        private bool isF = false;

        UnityAction endTutorial;
        Animator animator;
        Health health;                           // castle의 health 참조
        #endregion

        #endregion
        void Start()
        {
            health = castle.GetComponent<Health>();
            animator = showButton.GetComponent<Animator>();
            endTutorial += EndUI;
            guideText.fontSize = fontSize;
            buttonAnimAction += playShowButtonAnim;
            cmb = player.GetComponent<CinemachineBrain>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) || leftXButton.action.WasCompletedThisFrame() || leftMenuButton.action.WasCompletedThisFrame())
            {
                HideUI();
            }

            //if (leftYButton.action.WasCompletedThisFrame())
            //{
            //    HideUIWhenViewChange();
            //}

            // Step.A TopView 확인하기
            if (isA == true)
            {
                AChangeTopView();
                if (playerDummy.activeSelf == true)
                {
                    isA = false;
                    isB = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.B 곡괭이로 무기 바꾸기
            if (isB == true)
            {
                BChangeToPickax();
                if (PickAxe.activeSelf == true)
                {
                    isB = false;
                    isC = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.C 곡괭이로 채광하기
            if (isC == true)
            {
                CMiningRock();
                // 튜토리얼 Rock이 사라진 경우
                if (rock == null)
                {
                    isC = false;
                    isD = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.D 도끼로 벌목하기
            if (isD == true)
            {
                DLoggingTree();
                // 튜토리얼 Tree가 사라진 경우
                if (tree == null)
                {
                    isD = false;
                    isE = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.E User UI 띄우고 Build 선택하고 타워 건설하기
            if (isE == true)
            {
                EShowUserUI();
                TowerBase go = FindFirstObjectByType<TowerBase>();
                // 타워가 있는 경우
                if (go != null)
                {
                    isE = false;
                    isF = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.F 왼손 UI show 버튼 클릭 후 skip
            if (isF == true)
            {
                FSkipRoundTimer();
                // 왼손에 SHOW 버튼으로 UI 켜고 SKIP 버튼으로 라운드 시작
                if (lsm.waveCount > 0)
                {
                    isF = false;
                    guideString = $"Protect the castle from the enemy";
                    guideText.text = guideString;
                }
            }

            // 성이 부숴졌거나 enemy가 죽은 경우 튜토리얼 종료
            if (health.CurrentHealth <= 0 || (lsm.waveCount > 0 && ListSpawnManager.enemyAlive == 0))
            {
                endTutorial.Invoke();
            }
        }
        // Step.A TopView 확인하기
        void AChangeTopView()
        {
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to see the entire map";
            guideText.text = guideString;
        }

        // Step.B 곡괭이로 무기 바꾸기
        void BChangeToPickax()
        {
            guideText.spriteAsset = pickaxSpriteAsset;
            guideString = $"Press the <color=#FF0000>A</color>-Action button to change the   <size=12><sprite=0>";
            guideText.text = guideString;
        }

        // Step.C 곡괭이로 채광하기
        void CMiningRock()
        {
            guideString = $"Use    <size=12><sprite=0><size={fontSize}>to mine the rock";
            guideText.text = guideString;
        }

        // Step.D 도끼로 벌목하기
        void DLoggingTree()
        {
            guideText.spriteAsset = axeSpriteAsset;
            guideString = $"Change equipment into  <size=12><sprite=0><size={fontSize}> and logging";
            guideText.text = guideString;
        }

        // Step.E User UI 띄우고 Build 선택하고 타워 건설하기
        void EShowUserUI()
        {
            guideText.spriteAsset = handSpriteAsset;
            guideString = $"Change to    <size=12><sprite=0><size={fontSize}>\nPress the <color=#FF0000>X</color>-Action button to show the UI\nSelect Build, and use the Grab button to build a tower";
            guideText.text = guideString;
        }

        // Step.F 왼손 UI show 버튼 클릭 후 skip
        void FSkipRoundTimer()
        {
            guideString = "Turn on UI through <color=#FF0000>Show</color> button attached to left hand\n Start the round through the <color=#FF0000>Skip</color> button";
            guideText.text = guideString;
        }

        // Show UI
        public void ShowUI()
        {
            backgroundUI.SetActive(true);
            showButton.gameObject.SetActive(false);
            animator.SetBool(isNewGuide, false);
        }

        // Hide Guide UI
        public void HideUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(true);
        }

        // ViewChange에 따른 UI
        //public void HideUIWhenViewChange()
        //{
        //    if (cmb.enabled == false)
        //    {
        //        backgroundUI.SetActive(false);
        //        showButton.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        backgroundUI.SetActive(false);
        //        showButton.gameObject.SetActive(true);
        //    }
        //}

        // End UI
        public void EndUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(false);
            endTutorialUI.SetActive(true);
        }

        // Next
        public void OnClickNext()
        {
            SceneManager.LoadScene(loadToScene);
        }

        // Retry 
        public void OnClickRetry()
        {
            // 현재 활성화된 씬의 이름 가져오기
            string currentSceneName = SceneManager.GetActiveScene().name;
            // 씬 다시 로드
            SceneManager.LoadScene(currentSceneName);
        }

        // ShowButtonAnim 재생
        public void playShowButtonAnim()
        {
            animator.SetBool(isNewGuide, !backgroundUI.activeSelf);
        }
    }
}

