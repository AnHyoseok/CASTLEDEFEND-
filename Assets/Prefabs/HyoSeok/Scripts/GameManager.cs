using Defend.Enemy;
using Defend.Player;
using Defend.TestScript;
using MyVrSample;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        ListSpawnManager ListSpawnManager;
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
        public Data data; // 게임 데이터

        
        #endregion

        private void Start()
        {
            //참조
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            //시작하자마자 불러오기
            DataManager.Instance.LoadGameData();

            //세이브 버튼 
            //saveButton.onClick.AddListener(OnSaveButtonClicked);

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
            DataManager.Instance.SaveGameData();
        }

        public  void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //세이브 버튼용
        public void SaveGameData()
        {
            Debug.Log("Data: " + data);
            Debug.Log("DataManager Instance: " + DataManager.Instance);
            Debug.Log(data.health);
            Debug.Log(data.money);
            Debug.Log(data.tree);
            Debug.Log(data.rock);
            Debug.Log(playerState.money);
            //data.health = health.maxHealth;
            data.money = 30f;
            //data.tree = playerState.tree; 
            //data.rock = playerState.rock;


            // 데이터 저장
            DataManager.Instance.SaveGameData();
            Debug.Log("Game data saved.");
        }

        //게임클리어
        IEnumerator GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 &&ListSpawnManager.waveCount >=5)
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