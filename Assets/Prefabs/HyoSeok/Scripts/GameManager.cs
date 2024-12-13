using Defend.Enemy;
using Defend.Manager;
using Defend.Player;
using Defend.TestScript;
using MyVrSample;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        ListSpawnManager ListSpawnManager;
        CastleUpgrade castleUpgrade;
        Health health;
        public GameObject player;
        public GameObject castle;
        //ui
        public GameObject clearUI;
        public GameObject gameoverUI;

        //sfs
        private AudioSource audioSource;
        public AudioClip clearSound;
        public AudioClip gameoverSound;

        //테스트용 시간초
        public TextMeshProUGUI testtext;
        float time;

        //public Button saveButton; // 세이브 버튼
        public Data data = new Data(); // 게임 데이터


        #endregion

        private void Start()
        {
            //참조
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            castleUpgrade = Object.FindAnyObjectByType<CastleUpgrade>();
            //시작하자마자 불러오기
            //LoadGameData();
            //playerState.money = data.money;

            //....
            //...



            if (!audioSource)
                return;
            audioSource = player.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // 자동 재생 방지

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

            time += Time.deltaTime;
            testtext.text = time.ToString("F1");
        }

        //게임 종료시 자동저장
        private void OnApplicationQuit()
        {
            DataManager.Instance.SaveGameData(data);
        }

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //세이브 버튼용
        public void SaveGameData()
        {
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

            // 데이터 저장=
            DataManager.Instance.SaveGameData(data);
        }

        //로드 버튼
        public void LoadGameData()
        {
            // 데이터 로드
            DataManager.Instance.LoadGameData();

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
        }

        //데이터초기화
        public void DeleteGameData()
        {
            data = null;
            //데이터삭제
            DataManager.Instance.DeleteGameData(data);
        }

        //게임클리어
        IEnumerator GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 && ListSpawnManager.waveCount >= 5)
            {
                //게임클리어 창 뜨기
                clearUI.SetActive(true);

                //클리어 사운드
                audioSource.clip = clearSound;
                audioSource.Play();


                yield return new WaitForSeconds(3f);

                audioSource.Stop();
            }
        }
        //게임오버
        IEnumerator GameOver()
        {

            if (playerState.health <= 0)
            {
                //게임오버 창 띄우기
                gameoverUI.SetActive(true);
                //게임오버 사운드
                audioSource.clip = gameoverSound;
                audioSource.Play();
                yield return new WaitForSeconds(3f);
                audioSource?.Stop();
            }
        }
    }
}


