using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
namespace Defend.Player
{
    /// <summary>
    /// Castle상점 구현(업그레이드)
    /// </summary>
    public class CastleUpgrade : MonoBehaviour
    {
        #region Variables
        //참조
        HealthBasedCastle HealthBasedCastle;
        PlayerState playerState;
        Health health;
        public GameObject castle;
        //체젠속도
        private float interval;

        //포탈 활성화
        public GameObject potal;
        public Renderer[] potalsColor;
        public GameObject[] potalsEffect;

        //영구 구매여부
        private bool isPotalActive = false;
        private bool isMoveSpeedUp = false;
        private bool isMoneyUp = false;
        private bool isTreeUp = false;
        private bool isRockUp = false;

        //버튼 비활성화
        public Button[] btnList;

        //이동속도 변경
        public DynamicMoveProvider dynamicMove;
        #endregion
        private void Start()
        {

            //참조
            health = castle.GetComponent<Health>();

            for (int i = 0; i < potalsColor.Length; i++)
            {
                potalsColor[i] = GetComponent<Renderer>();
            }

            //btn = item.transform.Find("Button").GetComponent<Button>();

            //초기화
            interval = health.Rginterval;

            health.HPTime(interval, 1f);//1초마다 1의 체력을 회복

        }


        //체력 모두 회복
        public void FullonHP()
        {

            //100원 소모시 풀피 //풀피일때 구매 막기 
            if (health != null && health.CurrentHealth != health.maxHealth)
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(100f);
            }
            else
            {
                //버튼 비활성화 
                offBuyButton();
            }

        }

        //체력 올리기
        public void HPUpgrade()
        {
            if (health != null && health.maxHealth <= 1000f)
            {
                //100씩 올리기
                health.IncreaseMaxHealth(100f);
                //100골드 소비
                playerState.SpendMoney(100f);
            }
            else
            {
                //버튼 비활성화
                offBuyButton();
            }
        }

        //체력회복량 1초에 interval 만큼 회복
        public void HPTimeUpgrade()
        {

            if (health != null && interval <= 5f)
            {
                interval++;
            }
            else
            {
                //버튼 비활성화
                offBuyButton();
            }

        }

        //자원흭득량
        public void MoneyGain()
        {
            if(isMoneyUp == false)
            {
                ResourceManager.Instance.UpgradeResourceGain("money", 1.2f); //20% 더 얻기
                playerState.SpendMoney(100f);
                isMoneyUp = true;
            }
            else
            {
                //버튼 비활성화
                offBuyButton();
            }
        }
        public void TreeGain()
        {
            if (isTreeUp==false)
            {
                ResourceManager.Instance.UpgradeResourceGain("tree", 1.5f); //50% 더 얻기
                playerState.SpendMoney(100f);
                isTreeUp = true;
            }
            else
            {
                //버튼 비활성화
                offBuyButton();
            }
        }
        public void RockGain()
        {
            if (isRockUp ==false)
            {
                ResourceManager.Instance.UpgradeResourceGain("rock", 1.5f); //50% 더 얻기
                playerState.SpendMoney(100f);
                isRockUp = true;
            }
            else
            {
                //버튼 비활성화
                offBuyButton();
            }

        }



        //플레이어 이동속도업(신발모양)
        public void MoveSpeed()
        {
            if (isMoveSpeedUp == false)
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(100f);
                isMoveSpeedUp = true;
                //버튼 비활성화
                offBuyButton();
            }

        }

        //포탈 활성화
        public void PotalActivate()
        {
            if (isPotalActive == false)
            {
                for (int i = 0; i < potalsColor.Length; i++)
                {
                    potalsColor[i].material.color = Color.red;
                    potalsEffect[i].SetActive(true);
                }
                XRSimpleInteractable xRSimpleInteractable = potal.GetComponent<XRSimpleInteractable>();
                xRSimpleInteractable.enabled = true;
                playerState.SpendMoney(100f);
                isPotalActive = true;
                //버튼 비활성화
                offBuyButton();
            }

        }

        //아이템 흭득 범위 증가
        public void AutoGain()
        {

        }


        //버튼 비활성화
        private void offBuyButton()
        {
            btnList[1].interactable = false; // 버튼 클릭을 비활성
        }

        //버튼 활성화
        private void onBuyButton()
        {
            btnList[1].interactable = true; //버튼 클릭 활성화
        }

    }
}