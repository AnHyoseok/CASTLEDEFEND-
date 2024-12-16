using Defend.Enemy;
using Defend.TestScript;
using Defend.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Tutorial Scene�� �����ϴ� Manager
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
        public TMP_SpriteAsset axeSpriteAsset;
        public TMP_SpriteAsset pickaxSpriteAsset;
        public TMP_SpriteAsset handSpriteAsset;
        #endregion

        public GameObject player;               // Player
        public GameObject playerDummy;          // PlayerDummy TopView �νĿ� Obj
        public GameObject rock;                 // Ʃ�丮��� rock
        public GameObject tree;                 // Ʃ�丮��� tree
        public GameObject castle;               // Ʃ�丮��� castle
        public GameObject axe;                  // �÷��̾� Axe
        public GameObject PickAxe;              // �÷��̾� PickAxe
        public ListSpawnManager lsm;            // ListSpawnManager
        public float fontSize;                  // guideText font size
        private string guideString;             // UI�� ��Ÿ���� ����
        private Health health;                  // castle�� health ����
        [SerializeField] private string loadToScene; // ���� �� �ε� �� Scene

        #region Step ���� Bool Variables
        // TopVIew Ȯ���ϱ�
        private bool isA = true;
        // ��̷� �ٲٱ�
        private bool isB = false;
        // ä���ϱ�
        private bool isC = false;
        // �����ϱ�
        private bool isD = false;
        // User UI ����
        private bool isE = false;
        // ���� �����ϱ�
        private bool isF = false;

        UnityAction endTutorial;
        #endregion

        #endregion
        void Start()
        {
            health = castle.GetComponent<Health>();
            endTutorial += EndUI;
            guideText.fontSize = fontSize;
        }

        // TODO :: SHOW ��ư ��¦�Ÿ���, ��ġ ���
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                HideUI();
            }

            // Step.A TopView Ȯ���ϱ�
            if (isA == true)
            {
                AChangeTopView();
                if (playerDummy.activeSelf == true)
                {
                    isA = false;
                    isB = true;
                }
            }

            // Step.B ��̷� ���� �ٲٱ�
            if (isB == true)
            {
                BChangeToPickax();
                if (PickAxe.activeSelf == true)
                {
                    isB = false;
                    isC = true;
                }
            }

            // Step.C ��̷� ä���ϱ�
            if (isC == true)
            {
                CMiningRock();
                // Ʃ�丮�� Rock�� ����� ���
                if (rock == null)
                {
                    isC = false;
                    isD = true;
                }
            }

            // Step.D ������ �����ϱ�
            if (isD == true)
            {
                DLoggingTree();
                // Ʃ�丮�� Tree�� ����� ���
                if (tree == null)
                {
                    isD = false;
                    isE = true;
                }
            }

            // Step.E User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
            if (isE == true)
            {
                EShowUserUI();
                TowerBase go = FindFirstObjectByType<TowerBase>();
                // Ÿ���� �ִ� ���
                if (go != null)
                {
                    isE = false;
                    isF = true;
                }
            }

            // Step.F �޼� UI show ��ư Ŭ�� �� skip
            if (isF == true)
            {
                FSkipRoundTimer();
                // �޼տ� SHOW ��ư���� UI �Ѱ� SKIP ��ư���� ���� ����
                if (lsm.waveCount > 0)
                {
                    isF = false;
                    guideString = $"Protect the castle from the enemy";
                    guideText.text = guideString;
                }
            }

            // ���� �ν����ų� enemy�� ���� ��� Ʃ�丮�� ����
            if (health.CurrentHealth <= 0 || (lsm.waveCount > 0 && ListSpawnManager.enemyAlive == 0))
            {
                endTutorial.Invoke();
            }
        }
        // Step.A TopView Ȯ���ϱ�
        void AChangeTopView()
        {
            // TODO :: ���� Ű ������ 
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to see the entire map";
            guideText.text = guideString;
        }

        // Step.B ��̷� ���� �ٲٱ�
        void BChangeToPickax()
        {
            // TODO :: ���� Ű ������ 
            guideText.spriteAsset = pickaxSpriteAsset;
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to change the   <size=12><sprite=0>";
            guideText.text = guideString;
        }

        // Step.C ��̷� ä���ϱ�
        void CMiningRock()
        {
            guideString = $"Use    <size=12><sprite=0><size={fontSize}>to mine the rock";
            guideText.text = guideString;
        }

        // Step.D ������ �����ϱ�
        void DLoggingTree()
        {
            guideText.spriteAsset = axeSpriteAsset;
            guideString = $"Change equipment into  <size=12><sprite=0><size={fontSize}> and logging";
            guideText.text = guideString;
        }

        // Step.E User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
        void EShowUserUI()
        {
            // TODO :: ���� Ű ������
            guideText.spriteAsset = handSpriteAsset;
            guideString = $"Change to    <size=12><sprite=0><size={fontSize}>\nPress the <color=#FF0000>Y</color>-Action button to show the UI\nSelect Build , and build a tower";
            guideText.text = guideString;
        }

        // Step.F �޼� UI show ��ư Ŭ�� �� skip
        void FSkipRoundTimer()
        {
            // TODO :: �޼� UI �۵�Ȯ��, SKIP �۵� Ȯ���ϱ�
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
            // TODO :: Fader �ִ��� ?
            SceneManager.LoadScene(loadToScene);
        }
        // Retry 
        public void OnClickRetry()
        {
            // ���� Ȱ��ȭ�� ���� �̸� ��������
            string currentSceneName = SceneManager.GetActiveScene().name;
            // �� �ٽ� �ε�
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
