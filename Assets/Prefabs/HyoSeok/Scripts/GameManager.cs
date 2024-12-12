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
        //����
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

        //�׽�Ʈ�� �ð���
        public TextMeshProUGUI testtext;
        float time;

        //public Button saveButton; // ���̺� ��ư
        public Data data = new Data(); // ���� ������


        #endregion

        private void Start()
        {
            //����
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            //�������ڸ��� �ҷ�����

            //playerState.money = data.money;
            //���̺� ��ư 
            //saveButton.onClick.AddListener(OnSaveButtonClicked);

            if (!audioSource)
                return;
            audioSource = player.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // �ڵ� ��� ����

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

        //���� ����� �ڵ�����

        private void OnApplicationQuit()
        {
            DataManager.Instance.SaveGameData(data);
        }

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //���̺� ��ư��
        public void SaveGameData()
        {
            //Debug.Log("Data: " + data);
            //Debug.Log("DataManager Instance: " + DataManager.Instance);
            //Debug.Log(data.health);

            //Debug.Log(data.tree);
            //Debug.Log(data.rock);
            Debug.Log(playerState.money);
            //data.health = health.maxHealth;
            data.money = playerState.money;
            Debug.Log($"data.money={data.money}");
            //data.tree = playerState.tree; 
            //data.rock = playerState.rock;


            // ������ ����
            DataManager.Instance.SaveGameData(data);
            //Debug.Log("Game data saved.");
        }

        //�ε� ��ư
        public void LoadGameData()
        {
            // ������ �ε�
            DataManager.Instance.LoadGameData();


         
            playerState.money = data.money;
            Debug.Log($"Loaded Money: {data.money}");

        }


        //����Ŭ����
        IEnumerator GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 && ListSpawnManager.waveCount >= 5)
            {
                //����Ŭ���� â �߱�
                clearUI.SetActive(true);

                //Ŭ���� ����
                audioSource.clip = clearSound;
                audioSource.Play();


                yield return new WaitForSeconds(3f);

                audioSource.Stop();

            }

        }

        //���ӿ���
        IEnumerator GameOver()
        {

            if (playerState.health <= 0)
            {
                //���ӿ��� â ����
                gameoverUI.SetActive(true);
                //���ӿ��� ����
                audioSource.clip = gameoverSound;
                audioSource.Play();
                yield return new WaitForSeconds(3f);
                audioSource?.Stop();
            }

        }


    }
}


