using Defend.Enemy;
using Defend.Player;
using MyVrSample;
using Unity.VisualScripting;
using UnityEngine;

namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        SpawnManager SpawnManager;

        //ui
        public GameObject clearUI;
        public GameObject gameoverUI;

  

        //sfs
        public AudioSource clearSound;
        public AudioSource gameoverSound;
        #endregion



        void GameClear()
        {

            if (SpawnManager.enemyAlive <= 0)
            {
                //게임클리어 창 뜨기
                clearUI.SetActive(true);
            
                //동작정지
                Time.timeScale = 0; 
            }

        }

        void GameOver()
        {

            if (playerState.health <= 0)
            {
                //게임오버 창 띄우기
                gameoverUI.SetActive(true);
                //게임오버 사운드

                //동작정지
                Time.timeScale = 0;
            }


        }
    }
}