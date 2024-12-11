using Defend.TestScript;
using Defend.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        public TMP_SpriteAsset axeSpriteAsset;
        public TMP_SpriteAsset pickaxSpriteAsset;
        public TMP_SpriteAsset handSpriteAsset;
        #endregion

        public GameObject player;
        public GameObject rock;                 // 튜토리얼용 rock
        public GameObject tree;                 // 튜토리얼용 tree
        public GameObject castle;               // 튜토리얼용 castle
        public GameObject enemy;                // 튜토리얼용 enemy
        private string guideString;             // UI에 나타나는 문구
        private Health health;                  // castle의 health 참조
        public float fontSize;                  // guideText font size

        #region Step 진행 Bool Variables
        // 곡괭이로 바꾸기
        private bool isA = true;
        // 채광하기
        private bool isB = false;
        // 벌목하기
        private bool isC = false;
        // User UI 띄우기
        private bool isD = false;
        // 라운드 시작하기
        private bool isE = false;
        #endregion

        #endregion
        void Start()
        {
            health = castle.GetComponent<Health>();
        }

        // TODO :: SHOW 버튼 반짝거리기, 위치 잡기
        // TODO :: 탑뷰 해보기
        void Update()
        {
            guideText.fontSize = fontSize;
            // TODO :: UI 상호작용 하는법

            // Step.A 곡괭이로 무기 바꾸기
            if (isA == true)
            {
                AChangeToPickax();
                // TODO :: Player 손에 곡괭이가 생긴 경우
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isA = false;
                    isB = true;
                }
            }

            // Step.B 곡괭이로 채광하기
            if (isB == true)
            {
                BMiningRock();
                // 튜토리얼 Rock이 사라진 경우
                if (Input.GetKeyDown(KeyCode.B))
                //if (rock == null)
                {
                    isB = false;
                    isC = true;
                }
            }

            // Step.C 도끼로 벌목하기
            if (isC == true)
            {
                CLoggingTree();
                if (Input.GetKeyDown(KeyCode.C))
                // 튜토리얼 Tree가 사라진 경우
                //if (tree == null)
                {
                    isC = false;
                    isD = true;
                }
            }

            // Step.D User UI 띄우고 Build 선택하고 타워 건설하기
            if (isD == true)
            {
                DShowUserUI();
                TowerBase go = FindFirstObjectByType<TowerBase>();
                if (Input.GetKeyDown(KeyCode.D))
                // 타워가 있는 경우
                //if(go !=null)
                {
                    isD = false;
                    isE = true;
                }
            }

            // Step.E 왼손 UI show 버튼 클릭 후 skip
            if (isE == true)
            {
                ESkipRoundTimer();
                if (Input.GetKeyDown(KeyCode.F))
                // 왼손에 SHOW 버튼으로 UI 켜고 SKIP 버튼으로 라운드 시작
                // TODO :: Waypoint 만들기
                //if (enemy.activeSelf == true)
                {
                    isE = false;
                }
            }

            // 성이 부숴졌거나 enemy가 파괴된 경우 튜토리얼 종료
            if (health.CurrentHealth <= 0 || enemy == null)
            {
                // TODO :: 종료 Scene 이동
            }
        }

        // Step.A 곡괭이로 무기 바꾸기
        void AChangeToPickax()
        {
            // TODO :: 장비 바뀌는 거 확인, 무슨 키 쓰는지 
            guideText.spriteAsset = pickaxSpriteAsset;
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to change the   <size=12><sprite=0>";
            guideText.text = guideString;
        }

        // Step.B 곡괭이로 채광하기
        void BMiningRock()
        {
            // TODO :: 채광되는지 확인 
            guideString = $"Use    <size=12><sprite=0><size={fontSize}>to mine the rock";
            guideText.text = guideString;
        }

        // Step.C 도끼로 벌목하기
        void CLoggingTree()
        {
            // TODO :: 벌목되는지 확인 
            guideText.spriteAsset = axeSpriteAsset;
            guideString = $"Change equipment into  <size=12><sprite=0><size={fontSize}> and logging";
            guideText.text = guideString;
        }

        // Step.D User UI 띄우고 Build 선택하고 타워 건설하기
        void DShowUserUI()
        {
            // TODO :: 타워 건설하는법 이식하기, 무슨 키 쓰는지
            guideText.spriteAsset = axeSpriteAsset;
            //guideText.spriteAsset = handSpriteAsset;
            guideString = $"Change to <size=12><sprite=0><size={fontSize}>\nPress the <color=#FF0000>Y</color>-Action button to show the UI\nSelect Build , and build a tower";
            guideText.text = guideString;
        }

        // Step.E 왼손 UI show 버튼 클릭 후 skip
        void ESkipRoundTimer()
        {
            // TODO :: 왼손 UI 작동확인, TIMER 추가하기, SKIP 작동 확인하기, Enemy 활성화
            // SKIP은 단순 Enemy 활성화만 해도 가능
            guideString = "Turn on UI through <color=#FF0000>Show</color> button attached to left hand\n Start the round through the <color=#FF0000>Skip</color> button";
            guideText.text = guideString;
        }

        // Show UI
        public void ShowUI()
        {
            backgroundUI.SetActive(true);
            showButton.gameObject.SetActive(false);
        }
        // Hide UI
        public void HideUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(true);
        }
    }
}
