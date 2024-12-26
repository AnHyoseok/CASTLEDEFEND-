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
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Utillity;


namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //참조
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


        //public Button saveButton; // 세이브 버튼
        public Data data = new Data(); // 게임 데이터

        //이동속도 변경
        public DynamicMoveProvider dynamicMove;
        private bool isGameOver = false;
        private bool isGameClear = false;
        #endregion

        private void Start()
        {

            //참조
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
            //audioSource.playOnAwake = false; // 자동 재생 방지

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

        //게임 종료시 자동저장
        // 초기화임
        //private void OnApplicationQuit()
        //{
        //    DataManager.Instance.SaveGameData(data);
        //}

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //세이브 버튼용
        public void SaveGameData()
        {

            //스테이지 카운트
            data.Round = listSpawnManager.waveCount - 1;
            data.countdown = listSpawnManager.countdown;

            //플레이어 자원 여부
            data.health = health.maxHealth;
            data.money = playerState.money;
            data.tree = playerState.tree;
            data.rock = playerState.rock;
            //업그레이드 단계
            data.isHPUpgradelevel = castleUpgrade.currentHPUpgradeLevel;
            data.isHPTimeUpgradelevel = castleUpgrade.currentHPTimeUpgradeLevel;
            data.isArmorUpgradelevel = castleUpgrade.currenteArmorUpgradeLevel;
            data.isATKUpgradelevel = castleUpgrade.currentTowerATKUpgradeLevel;
            data.isATKSpeedUpgradelevel = castleUpgrade.currentTowerATKSpeedUpgradeLevel;
            data.isATKRangeUpgradelevel = castleUpgrade.currentTowerATKRangeUpgradeLevel;
            data.isMoneyGainlevel = castleUpgrade.currentMoneyGainUpgradeLevel;
            data.isTreeGainlevel = castleUpgrade.currentTreeGainUpgradeLevel;
            data.isRockGainlevel = castleUpgrade.currentRockGainUpgradeLevel;
            //업그레이드 해금여부
            data.isPotalActive = castleUpgrade.isPotalActive;
            data.isMoveSpeedUp = castleUpgrade.isMoveSpeedUp;
            data.isAutoGain = castleUpgrade.isAutoGain;
            //타워 해금여부

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

            //타워UI해금
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

            ////사운드
            //Debug.Log(AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER) + "savedata");
            //data.soundSettings[Constants.AUDIO_UTIL_MASTER] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER);
            //data.soundSettings[Constants.AUDIO_UTIL_BGM] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM);
            //data.soundSettings[Constants.AUDIO_UTIL_EFFECT] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT);
            data.initialSound(
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER),
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM),
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT)
                    );

            //터널링
            data.isTuneeling = toggleButton.isOnto;
            //플레이어ui
            data.isPlayerUI = toggleButton.isOnPlay;

            // 데이터 저장=
            DataManager.Instance.SaveGameData(data);
        }

        //로드 버튼
        public void LoadGameData()
        {
            // 데이터 로드
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

            //필드에 있는 자원들
            DropItem[] item = FindObjectsByType<DropItem>(FindObjectsSortMode.None);
            foreach (DropItem e in item)
            {
                Destroy(e.gameObject);
            }
            //진행 라운드 수
            listSpawnManager.waveCount = data.Round;
            listSpawnManager.countdown = data.countdown;
            //플레이어 자원 여부
            health.maxHealth = data.health;
            playerState.money = data.money;
            playerState.tree = data.tree;
            playerState.rock = data.rock;
            //업그레이드 단계
            castleUpgrade.currentHPUpgradeLevel = data.isHPUpgradelevel;
            castleUpgrade.currentHPTimeUpgradeLevel = data.isHPTimeUpgradelevel;
            castleUpgrade.currenteArmorUpgradeLevel = data.isArmorUpgradelevel;
            castleUpgrade.currentTowerATKUpgradeLevel = data.isATKUpgradelevel;
            castleUpgrade.currentTowerATKSpeedUpgradeLevel = data.isATKSpeedUpgradelevel;
            castleUpgrade.currentTowerATKRangeUpgradeLevel = data.isATKRangeUpgradelevel;
            castleUpgrade.currentMoneyGainUpgradeLevel = data.isMoneyGainlevel;
            castleUpgrade.currentTreeGainUpgradeLevel = data.isTreeGainlevel;
            castleUpgrade.currentRockGainUpgradeLevel = data.isRockGainlevel;
            //업그레이드 해금여부
            castleUpgrade.isPotalActive = data.isPotalActive;
            castleUpgrade.isMoveSpeedUp = data.isMoveSpeedUp;
            castleUpgrade.isAutoGain = data.isAutoGain;
            //타워 해금여부

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

            //타워 버튼 해금
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

            //타워들
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (var tower in towerbase)
            {
                Destroy(tower.gameObject);
                buildManager.playerState.AddMoney(tower.GetTowerInfo().GetSellCost());

            }
            //사운드
            //float masterSound = data.soundSettings[Constants.AUDIO_UTIL_MASTER];
            //Debug.Log(masterSound+"loaddata");
            //AudioUtility.SetVolume(masterSound, Constants.AUDIO_UTIL_MASTER);
            //audioManager.m_AudioMasterSlider.value = masterSound;

            //float bgmSound = data.soundSettings[Constants.AUDIO_UTIL_BGM];
            //AudioUtility.SetVolume(bgmSound, Constants.AUDIO_UTIL_BGM);
            //audioManager.m_AudioBGMSlider.value = bgmSound;

            //float effectSound = data.soundSettings[Constants.AUDIO_UTIL_EFFECT];
            //AudioUtility.SetVolume(effectSound, Constants.AUDIO_UTIL_EFFECT);
            //audioManager.m_AudioSFXSlider.value = effectSound;
            AudioUtility.SetVolume(data.soundData.masterVolumn, Constants.AUDIO_UTIL_MASTER);
            AudioUtility.SetVolume(data.soundData.bgmVolumn, Constants.AUDIO_UTIL_BGM);
            AudioUtility.SetVolume(data.soundData.sfxVolumn, Constants.AUDIO_UTIL_EFFECT);

            //터널링
            toggleButton.isOnto = data.isTuneeling;
            //플레이어ui
            toggleButton.isOnPlay = data.isPlayerUI;
        }

        //다시하기 
        public void RestartGame()
        {
            // 데이터 초기화
            DataManager.Instance.SaveGameData(new Data());
            //씬다시시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // 게임 종료
        public void QuitGame()
        {

            Application.Quit();
        }
        //게임클리어
        void GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 && listSpawnManager.waveCount >= 4 && !isGameClear)
            {
                isGameClear = true;
                //Debug.Log("GameClaer");
                clearUI.SetActive(true);
            }
        }
        void GameOverGo()
        {
            if (health.CurrentHealth <= 0 && !isGameOver)
            {
                health.RgAmount = 0;
                isGameOver = true;
                gameoverUI.SetActive(true);
           
            }
        }



        //치트용 버튼
        public void Cheating()
        {
            playerState.money = 999999f;
            playerState.tree = 999999f;
            playerState.rock = 999999f;
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

            dynamicMove.moveSpeed = 10f;
            //player.transform.position = new Vector3(0f, 4f, -6.0f);
        }

        //플레이어 위치 셋 
        public void PlayerTransformSenter()
        {
            player.transform.position = new Vector3(0f, 4f, -6.0f);
        }
    }
}


