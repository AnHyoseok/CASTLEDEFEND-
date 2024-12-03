using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
using TMPro;
using UnityEngine.Playables;
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
        private float RgAmount;

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
        private bool isAutoGain = false;
        //버튼 비활성화
        public Button[] btnList;

        //이동속도 변경
        public DynamicMoveProvider dynamicMove;

        //비용
        private float upgradeCost1 = 100f;  //임시용
        //private float CostFullHP = 100f;    //hp회복
        private float[] CostHPUpgrade = { 100f, 250f, 500f };
        //private float[] CostHPTimeUpgrade = { 250f, 500f, 1000f };
        //private float[] CostMoneyGainUpgrade = { 100f, 250f, 500f };
        //private float[] CostTreeGainUpgrade = { 100f, 250f, 500f };
        //private float[] CostRockGainUpgrade = { 100f, 250f, 500f };
        //private float CostMoveSpeedUpgrade = 200f;
        //private float CostPotalActivateUpgrade = 300f;
        //private float CostAutoGainUpgrade = 400f;

        //업그레이드별 수치증가량
        private float[] IncreaseHPUpgrade = { 100f, 200f, 300f }; //hp
        //private float[] IncreaseHPTimeUpgrade = { 1f, 3f, 5f }; //초당 2, 5 ,10
        //private float[] IncreaseMoneyGainUpgrade = { };
        //private float[] IncreaseTreeGainUpgrade = { };
        //private float[] increaseRockGainUpgrade = { };

        //업그레이드 단계
        private int currentHPUpgradeLevel = 0; //hp
        //private int currentHPTimeUpgradeLevel = 0; //체젠
        //private int currentMoneyGainUpgrade = 0;
        //private int currentTreeGainUpgrade = 0;
        //private int currentRockGainUpgrade = 0;

        //테스트용 텍스트
        public TextMeshProUGUI test1;
        public TextMeshProUGUI test2;
        public TextMeshProUGUI test3;
        public TextMeshProUGUI test4;
      
        #endregion
        private void Start()
        {

            //참조
            health = castle.GetComponent<Health>();
            playerState = gameObject.GetComponent<PlayerState>();
            //초기화
            RgAmount = health.RgAmount;


            UpdateButtonStates();

        }

        private void Update()
        {
            test1.text = $"curent HP = {health.CurrentHealth}";
            test2.text = $"hpmax ={health.maxHealth}";
            test3.text = $"hp rex={health.RgAmount}";
            test4.text = $"curent money = {playerState.money}";
        }

        //체력 모두 회복
        public void FullonHP()
        {
            //Debug.Log("fullonhp spend money");
            //100원 소모시 풀피 //풀피일때 구매 막기 
            if (health != null && health.CurrentHealth != health.maxHealth && playerState.SpendMoney(upgradeCost1))
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(100f);
                UpdateButtonStates();
            }

        }

        //체력 올리기
        public void HPUpgrade()
        {
            //Debug.Log("HPUpgrade spend money");
            if (currentHPUpgradeLevel < CostHPUpgrade.Length && health != null && health.maxHealth < 1000f && playerState.SpendMoney(CostHPUpgrade[currentHPUpgradeLevel]))
            {
                // 단계별 체력 증가
                health.IncreaseMaxHealth(IncreaseHPUpgrade[currentHPUpgradeLevel]);
                playerState.SpendMoney(CostHPUpgrade[currentHPUpgradeLevel]);
                currentHPUpgradeLevel++;
                UpdateButtonStates();
            }

        }

        //체력회복량 1초에 interval 만큼 회복
        public void HPTimeUpgrade()
        {
            //Debug.Log("HPTimeUpgrade spend money");
            if (health != null && RgAmount < 5f && playerState.SpendMoney(upgradeCost1))
            {
                RgAmount++;
                health.HPTime(RgAmount, 1f);
                UpdateButtonStates();
            }


        }

        //자원흭득량
        public void MoneyGain()
        {
            //Debug.Log("MoneyGain spend money");
            if (isMoneyUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("money", 1.2f); //20% 더 얻기
                playerState.SpendMoney(100f);
                isMoneyUp = true;
                UpdateButtonStates();
            }
        }
        public void TreeGain()
        {
            //Debug.Log("TreeGain spend money");
            if (isTreeUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("tree", 1.5f); //50% 더 얻기
                playerState.SpendMoney(100f);
                isTreeUp = true;
                UpdateButtonStates();
            }
        }
        public void RockGain()
        {
            //Debug.Log("RockGain spend money");
            if (isRockUp == false && playerState.SpendMoney(upgradeCost1))
            {
                ResourceManager.Instance.UpgradeResourceGain("rock", 1.5f); //50% 더 얻기
                playerState.SpendMoney(100f);
                isRockUp = true;
                UpdateButtonStates();
            }


        }

        //플레이어 이동속도업(신발모양)
        public void MoveSpeed()
        {
            //Debug.Log("MoveSpeed spend money");
            if (isMoveSpeedUp == false && playerState.SpendMoney(upgradeCost1))
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(100f);
                isMoveSpeedUp = true;
                UpdateButtonStates();
            }

        }

        //포탈 활성화
        public void PotalActivate()
        {
            //Debug.Log("PotalActivate spend money");
            if (isPotalActive == false && playerState.SpendMoney(upgradeCost1))
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
                UpdateButtonStates();
            }

        }

        //아이템 흭득 범위 증가
        public void AutoGain()
        {

        }


        //버튼 상태 업데이트
        private void UpdateButtonStates()
        {
            
            btnList[0].interactable = playerState.money > upgradeCost1 && health.CurrentHealth != health.maxHealth; // FullHP 버튼
            btnList[1].interactable = currentHPUpgradeLevel < CostHPUpgrade.Length && health.maxHealth < 1000f && playerState.money > (CostHPUpgrade[currentHPUpgradeLevel]); // HPUpgrade 버튼
            btnList[2].interactable = playerState.money > upgradeCost1 && RgAmount < 5f; // HPTimeUpgrade 버튼
            btnList[3].interactable = playerState.money > upgradeCost1 && !isMoneyUp; // MoneyGain 버튼
            btnList[4].interactable =playerState.money > upgradeCost1 && !isTreeUp; // TreeGain 버튼
            btnList[5].interactable = playerState.money > upgradeCost1 && !isRockUp; // RockGain 버튼
            btnList[6].interactable = playerState.money > upgradeCost1 && !isMoveSpeedUp; // MoveSpeed 버튼
            btnList[7].interactable = playerState.money > upgradeCost1 && !isPotalActive; // PotalActivate 버튼
            btnList[8].interactable = playerState.money > upgradeCost1 && !isAutoGain; //AutoGain 버튼
        }

    }
}