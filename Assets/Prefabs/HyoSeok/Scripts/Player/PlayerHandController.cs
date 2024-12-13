using Defend.Enemy;
using Defend.Player;
using TMPro;
using Unity.Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class PlayerHandController : MonoBehaviour
    {
        #region Variables
        //참조
        PlayerState playerState;
        ListSpawnManager listSpawnManager;
        public TextMeshProUGUI[] texts;
  
        public GameObject uiOnButton;
        public GameObject viewCanvas;

  
        //버튼 켜지는여부
        private bool isOnUi;

        #endregion

        private void Start()
        {
            //참조
            playerState = GetComponent<PlayerState>();
            listSpawnManager = Object.FindAnyObjectByType<ListSpawnManager>();
        

        }

        private void Update()
        {

            if (listSpawnManager != null)
            {
                texts[0].text = listSpawnManager.countdown.ToString("F0"); //스폰타이머
            }
            texts[1].text = playerState.FormatMoney();  //돈
            texts[2].text = playerState.tree.ToString();  //나무
            texts[3].text = playerState.rock.ToString();  //돌
            //texts[4].text = listSpawnManager.waveCount.ToString();  //현재라운드
            texts[5].text = ListSpawnManager.enemyAlive.ToString();  //현재남아있는적의수

        }



        //ui 켜기
        public void ShowButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
              
            }
            else
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);
            
            }
        }

        //ui 끄기
        public void hideButton()
        {
            if (isOnUi == true)
            {
                uiOnButton.SetActive(false);
                viewCanvas.SetActive(true);

            }
            else
            {
                uiOnButton.SetActive(true);
                viewCanvas.SetActive(false);
              
            }
        }

      

       

    }
}