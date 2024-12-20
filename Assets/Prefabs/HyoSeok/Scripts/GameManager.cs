using Defend.Enemy;
using UnityEngine;
using Defend.Player;
using Defend.TestScript;
using Defend.UI;
using System;
using System.Collections;
using TMPro;
using Defend.Tower;
using Defend.Interactive;
using Defend.item;
using UnityEngine.SceneManagement;


namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        TowerBase[] towerbase;
        ListSpawnManager listSpawnManager;
        CastleUpgrade castleUpgrade;
        BuildMenu build;
        Health health;
        TowerBuildMenuName towerBuildMenuName;
        GameResources resources;
        HealthBasedCastle healthBasedCastle;
        ToggleButton toggleButton;
        private GameObject[] enemies;
        EnemyState[] enemyState;
        private BuildManager buildManager;
        public GameObject player;
        public GameObject castle;
        //ui
        public GameObject clearUI;
        public GameObject gameoverUI;

        //sfs
        //private AudioSource audioSource;
        //public AudioClip clearSound;
        //public AudioClip gameoverSound;


        //public Button saveButton; // ���̺� ��ư
        public Data data = new Data(); // ���� ������


        private bool isGameOver = false;
        private bool isGameClear = false;
        #endregion

        private void Start()
        {
            //����
            health = castle.GetComponent<Health>();
            playerState = FindAnyObjectByType<PlayerState>();
            castleUpgrade = FindAnyObjectByType<CastleUpgrade>();
            listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
            build = FindAnyObjectByType<BuildMenu>();
            towerBuildMenuName = FindAnyObjectByType<TowerBuildMenuName>();
            healthBasedCastle = FindAnyObjectByType<HealthBasedCastle>();
            toggleButton = FindAnyObjectByType<ToggleButton>();
            buildManager = BuildManager.instance;

            //LoadGameData();


            //if (!audioSource)
            //    return;
            //audioSource = player.AddComponent<AudioSource>();
            //audioSource.playOnAwake = false; // �ڵ� ��� ����

        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                Time.timeScale = 10f;
            }
            if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                Time.timeScale = 1f;
            }

            if (isGameOver)
                return;

            GameOverGo();
            GameClear();
        }

        //���� ����� �ڵ�����
        // �ʱ�ȭ��
        //private void OnApplicationQuit()
        //{
        //    DataManager.Instance.SaveGameData(data);
        //}

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //���̺� ��ư��
        public void SaveGameData()
        {

            //�������� ī��Ʈ
            data.Round = listSpawnManager.waveCount - 1;
            data.countdown = listSpawnManager.countdown;

            //�÷��̾� �ڿ� ����
            data.health = health.maxHealth;
            data.money = playerState.money;
            data.tree = playerState.tree;
            data.rock = playerState.rock;
            //���׷��̵� �ܰ�
            data.isHPUpgradelevel = castleUpgrade.currentHPUpgradeLevel;
            data.isHPTimeUpgradelevel = castleUpgrade.currentHPTimeUpgradeLevel;
            data.isArmorUpgradelevel = castleUpgrade.currenteArmorUpgradeLevel;
            data.isATKUpgradelevel = castleUpgrade.currentTowerATKUpgradeLevel;
            data.isATKSpeedUpgradelevel = castleUpgrade.currentTowerATKSpeedUpgradeLevel;
            data.isATKRangeUpgradelevel = castleUpgrade.currentTowerATKRangeUpgradeLevel;
            data.isMoneyGainlevel = castleUpgrade.currentMoneyGainUpgradeLevel;
            data.isTreeGainlevel = castleUpgrade.currentTreeGainUpgradeLevel;
            data.isRockGainlevel = castleUpgrade.currentRockGainUpgradeLevel;
            //���׷��̵� �رݿ���
            data.isPotalActive = castleUpgrade.isPotalActive;
            data.isMoveSpeedUp = castleUpgrade.isMoveSpeedUp;
            data.isAutoGain = castleUpgrade.isAutoGain;
            //Ÿ�� �رݿ���

            data.isTowerUnlock1 = build.towerinfo[3].isLock;
            data.isTowerUnlock2 = build.towerinfo[6].isLock;
            data.isTowerUnlock3 = build.towerinfo[9].isLock;
            data.isTowerUnlock4 = build.towerinfo[12].isLock;
            data.isTowerUnlock5 = build.towerinfo[15].isLock;
            data.isTowerUnlock6 = build.towerinfo[18].isLock;
            data.isTowerUnlock7 = build.towerinfo[21].isLock;
            //data.isTowerUnlock8 = build.towerinfo[8].isLock;
            //data.isTowerUnlock9 = build.towerinfo[9].isLock;
            //data.isTowerUnlock10 = build.towerinfo[10].isLock;
            //data.isTowerUnlock11 = build.towerinfo[11].isLock;
            //data.isTowerUnlock12 = build.towerinfo[12].isLock;

            //Ÿ��UI�ر�
            data.isTowerUnlocked1 = towerBuildMenuName.unlockTowerButton[0];
            data.isTowerUnlocked2 = towerBuildMenuName.unlockTowerButton[1];
            data.isTowerUnlocked3 = towerBuildMenuName.unlockTowerButton[2];
            data.isTowerUnlocked4 = towerBuildMenuName.unlockTowerButton[3];
            data.isTowerUnlocked5 = towerBuildMenuName.unlockTowerButton[4];
            data.isTowerUnlocked6 = towerBuildMenuName.unlockTowerButton[5];
            data.isTowerUnlocked7 = towerBuildMenuName.unlockTowerButton[6];
            //data.isTowerUnlocked8 = towerBuildMenuName.unlockTowerButton[7];
            //data.isTowerUnlocked9 = towerBuildMenuName.unlockTowerButton[8];
            //data.isTowerUnlocked10 = towerBuildMenuName.unlockTowerButton[9];
            //data.isTowerUnlocked11 = towerBuildMenuName.unlockTowerButton[10];
            //data.isTowerUnlocked12 = towerBuildMenuName.unlockTowerButton[12];

            ////����
            //data.soundSettings["Master"] = 0f;
            //data.soundSettings["BGM"] = 0f;
            //data.soundSettings["SFX"] = 0f;

            //�ͳθ�
            data.isTuneeling = toggleButton.isOnto;
            //�÷��̾�ui
            data.isPlayerUI = toggleButton.isOnPlay;

            // ������ ����=
            DataManager.Instance.SaveGameData(data);
        }

        //�ε� ��ư
        public void LoadGameData()
        {
            // ������ �ε�
            data = DataManager.Instance.LoadGameData();

            //enemys
            EnemyState[] enemys = FindObjectsByType<EnemyState>(FindObjectsSortMode.None);
            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue;
                }
                //e.gameObject.transform
                //e.gameObject.GetComponent<EnemyMoveController>().enabled = false;

                Destroy(e.gameObject);
            }
            ListSpawnManager.enemyAlive = 0;

            //�ʵ忡 �ִ� �ڿ���
            DropItem[] item = FindObjectsByType<DropItem>(FindObjectsSortMode.None);
            foreach (DropItem e in item)
            {
                Destroy(e.gameObject);
            }
            //���� ���� ��
            listSpawnManager.waveCount = data.Round;
            listSpawnManager.countdown = data.countdown;
            //�÷��̾� �ڿ� ����
            health.maxHealth = data.health;
            playerState.money = data.money;
            playerState.tree = data.tree;
            playerState.rock = data.rock;
            //���׷��̵� �ܰ�
            castleUpgrade.currentHPUpgradeLevel = data.isHPUpgradelevel;
            castleUpgrade.currentHPTimeUpgradeLevel = data.isHPTimeUpgradelevel;
            castleUpgrade.currenteArmorUpgradeLevel = data.isArmorUpgradelevel;
            castleUpgrade.currentTowerATKUpgradeLevel = data.isATKUpgradelevel;
            castleUpgrade.currentTowerATKSpeedUpgradeLevel = data.isATKSpeedUpgradelevel;
            castleUpgrade.currentTowerATKRangeUpgradeLevel = data.isATKRangeUpgradelevel;
            castleUpgrade.currentMoneyGainUpgradeLevel = data.isMoneyGainlevel;
            castleUpgrade.currentTreeGainUpgradeLevel = data.isTreeGainlevel;
            castleUpgrade.currentRockGainUpgradeLevel = data.isRockGainlevel;
            //���׷��̵� �رݿ���
            castleUpgrade.isPotalActive = data.isPotalActive;
            castleUpgrade.isMoveSpeedUp = data.isMoveSpeedUp;
            castleUpgrade.isAutoGain = data.isAutoGain;
            //Ÿ�� �رݿ���

            build.towerinfo[3].isLock = data.isTowerUnlock1;
            build.towerinfo[6].isLock = data.isTowerUnlock2;
            build.towerinfo[9].isLock = data.isTowerUnlock3;
            build.towerinfo[12].isLock = data.isTowerUnlock4;
            build.towerinfo[15].isLock = data.isTowerUnlock5;
            build.towerinfo[18].isLock = data.isTowerUnlock6;
            build.towerinfo[21].isLock = data.isTowerUnlock7;
            //build.towerinfo[8].isLock = data.isTowerUnlock8;
            //build.towerinfo[9].isLock = data.isTowerUnlock9;
            //build.towerinfo[10].isLock = data.isTowerUnlock10;
            //build.towerinfo[11].isLock = data.isTowerUnlock11;
            //build.towerinfo[12].isLock = data.isTowerUnlock12;

            //Ÿ�� ��ư �ر�
            towerBuildMenuName.unlockTowerButton[0].interactable = data.isTowerUnlocked1;
            towerBuildMenuName.unlockTowerButton[1].interactable = data.isTowerUnlocked2;
            towerBuildMenuName.unlockTowerButton[2].interactable = data.isTowerUnlocked3;
            towerBuildMenuName.unlockTowerButton[3].interactable = data.isTowerUnlocked4;
            towerBuildMenuName.unlockTowerButton[4].interactable = data.isTowerUnlocked5;
            towerBuildMenuName.unlockTowerButton[5].interactable = data.isTowerUnlocked6;
            towerBuildMenuName.unlockTowerButton[6].interactable = data.isTowerUnlocked7;
            //towerBuildMenuName.unlockTowerButton[7].interactable = data.isTowerUnlocked8;
            //towerBuildMenuName.unlockTowerButton[8].interactable = data.isTowerUnlocked9;
            //towerBuildMenuName.unlockTowerButton[9].interactable = data.isTowerUnlocked10;
            //towerBuildMenuName.unlockTowerButton[10].interactable = data.isTowerUnlocked11;
            //towerBuildMenuName.unlockTowerButton[12].interactable = data.isTowerUnlocked12;

            //Ÿ����
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (var tower in towerbase)
            {
                Destroy(tower.gameObject);
                buildManager.playerState.AddMoney(tower.GetTowerInfo().GetSellCost());

            }
            ////����
            //0f = data.soundSettings["Master"];
            //0f = data.soundSettings["BGM"];
            //0f = data.soundSettings["SFX"];

            //�ͳθ�
            toggleButton.isOnto = data.isTuneeling;
            //�÷��̾�ui
            toggleButton.isOnPlay = data.isPlayerUI;
        }

        //�ٽ��ϱ� 
        public void RestartGame()
        {
            // ������ �ʱ�ȭ
            DataManager.Instance.SaveGameData(new Data());
            //���ٽý���
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // ���� ����
        public void QuitGame()
        {

            Application.Quit();
        }

        //����Ŭ����
        void GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 && listSpawnManager.waveCount >= 4 && !isGameClear)
            {
                isGameClear = true;
                Debug.Log("GameClaer");
                //����Ŭ���� â �߱�
                clearUI.SetActive(true);

                ////Ŭ���� ����
                //audioSource.clip = clearSound;
                //audioSource.Play();

            }
        }
        void GameOverGo()
        {
            if (healthBasedCastle.castleHealth <= 0 && !isGameOver)
            {
                isGameOver = true;
                Debug.Log("GameOver");
                //���ӿ��� â ����
                gameoverUI.SetActive(true);
                //���ӿ��� ����
                //audioSource.clip = gameoverSound;
                //audioSource.Play(3);
            }
        }



        //ġƮ�� ��ư
        public void Cheating()
        {
            playerState.money = 99999f;
            towerBuildMenuName.unlockTowerButton[0].interactable = true;
            towerBuildMenuName.unlockTowerButton[1].interactable = true;
            towerBuildMenuName.unlockTowerButton[2].interactable = true;
            towerBuildMenuName.unlockTowerButton[3].interactable = true;
            towerBuildMenuName.unlockTowerButton[4].interactable = true;
            towerBuildMenuName.unlockTowerButton[5].interactable = true;
            towerBuildMenuName.unlockTowerButton[6].interactable = true;
            build.towerinfo[3].isLock = true;
            build.towerinfo[6].isLock = true;
            build.towerinfo[9].isLock = true;
            build.towerinfo[12].isLock = true;
            build.towerinfo[15].isLock = true;
            build.towerinfo[18].isLock = true;
            build.towerinfo[21].isLock = true;
            castleUpgrade.isPotalActive = true;
            castleUpgrade.isMoveSpeedUp = true;
            castleUpgrade.isAutoGain = true;
            //player.transform.position = new Vector3(0f, 4f, -6.0f);
        }

        //�÷��̾� ��ġ �� 
        public void PlayerTransformSenter()
        {
            player.transform.position = new Vector3(0f, 4f, -6.0f);
        }
    }
}


