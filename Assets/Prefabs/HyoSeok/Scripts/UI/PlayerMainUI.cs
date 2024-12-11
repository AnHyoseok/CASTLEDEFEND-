using Defend.Enemy;
using Defend.Player;
using Defend.TestScript;
using TMPro;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerMainUI : MonoBehaviour
    {
        #region Variables
        public GameObject castle;
        Health health;
        ListSpawnManager listSpawnManager;
        private PlayerState playerState;
        public TextMeshProUGUI[] texts;

        #endregion
        private void Start()
        {
            playerState = Object.FindAnyObjectByType<PlayerState>();
            listSpawnManager = Object.FindAnyObjectByType<ListSpawnManager>();
            health = castle.GetComponent<Health>();

        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                listSpawnManager.SkipTimer();
            }

            texts[0].text = health.CurrentHealth.ToString();  //체력
            texts[1].text = playerState.FormatMoney();  //돈
            texts[2].text = playerState.tree.ToString();  //나무
            texts[3].text = playerState.rock.ToString();  //돌
            if (listSpawnManager != null)
            {
                texts[4].text = listSpawnManager.countdown.ToString("F0"); //스폰타이머
            }
        }
    }
}