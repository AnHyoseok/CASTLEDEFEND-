using Defend.TestScript;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Interactive;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;
using Defend.Projectile;
using Defend.Tower;
namespace Defend.Player
{
    /// <summary>
    /// Castle상점 구현(업그레이드)
    /// </summary>
    public class CastleUpgrade : MonoBehaviour
    {
        #region Variables
        //참조
        public static BuffContents buffContents = new BuffContents();
        HealthBasedCastle HealthBasedCastle;
        TowerBase[] towerbase;
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
        public bool isPotalActive = false;
        public bool isMoveSpeedUp = false;
        public bool isAutoGain = false;
        //private bool isMoneyUp = false;
        //private bool isTreeUp = false;
        //private bool isRockUp = false;
        //버튼 비활성화
        public Button[] btnList;

        //이동속도 변경
        public DynamicMoveProvider dynamicMove;

        //비용
        //private float upgradeCost1 = 100f;  //임시용
        private float CostFullHP = 100f;    //hp회복
        private float[] CostHPUpgrade = { 100f, 250f, 500f };
        private float[] CostHPTimeUpgrade = { 250f, 500f, 1000f };
        private float[] CostArmorUpgrade = { 250f, 500f, 1000f }; //방어력

        // 타워 업그레이드
        private float[] CostTowerATKUpgrade = new float[50];
        private float[] CostTowerATKSpeed = new float[50];
        private float[] CostTowerATKRange = new float[50];


        //자원업그레이드
        private float[] CostMoneyGainUpgrade = { 100f, 250f, 500f };
        private float[] CostTreeGainUpgrade = { 100f, 250f, 500f };
        private float[] CostRockGainUpgrade = { 100f, 250f, 500f };

        //영구업그레이드
        private float CostMoveSpeedUpgrade = 200f;
        private float CostPotalActivateUpgrade = 300f;
        private float CostAutoGainUpgrade = 400f;



        //업그레이드별 수치증가량
        private float[] IncreaseHPUpgrade = { 100f, 200f, 300f }; //hp
        private float[] IncreaseHPTimeUpgrade = { 1f, 3f, 5f }; //초당 2, 5 ,10
        private float[] IncreaseMoneyGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] IncreaseTreeGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] increaseRockGainUpgrade = { 1.1f, 1.2f, 1.5f };
        private float[] increaseArmorUpgrade = { 5f, 10f, 20f };
        //private float increaseTowerATKUpgrade = 1f;
        //private float increaseTowerATKSpeedUpgrade = 1f;
        //private float increaseTowerATKRangeUpgrade = 1f;

        //업그레이드 단계
        public int currentHPUpgradeLevel = 0; //hp
        public int currentHPTimeUpgradeLevel = 0; //체젠
        public int currentMoneyGainUpgradeLevel = 0;
        public int currentTreeGainUpgradeLevel = 0;
        public int currentRockGainUpgradeLevel = 0;
        public int currenteArmorUpgradeLevel = 0;
        public int currentTowerATKUpgradeLevel = 0;
        public int currentTowerATKSpeedUpgradeLevel = 0;
        public int currentTowerATKRangeUpgradeLevel = 0;

        //타워업그레이드용
        public int atkLevel = 0;
        public int atkSpeedLevel = 0;
        public int atkRangeLevel = 0;

        //테스트용 텍스트
        public TextMeshProUGUI test1;
        public TextMeshProUGUI test2;
        public TextMeshProUGUI test3;
        public TextMeshProUGUI test4;

        //각 비용 텍스트
        public TextMeshProUGUI[] PriceTexts;
        #endregion
        private void Start()
        {

            //참조
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            //초기화
            RgAmount = health.RgAmount;


       
            initializeUpgradeCosts();
    

        }

        private void Update()
        {
            UpdateButtonStates();
            PriceUpdate();
            //UpdateButtonStates();
            test1.text = $"curent HP = {health.CurrentHealth}";
            test2.text = $"hpmax ={health.maxHealth}";
            test3.text = $"hp rex={health.RgAmount}";
            test4.text = $"curent money = {playerState.FormatMoney()}";
        }

        //타워 업그레이드 가격변동
        private void initializeUpgradeCosts()
        {
            float baseCost = 100f; // 기본 가격
            float increment = 50f;  // 업그레이드 시 증가하는 가격

            for (int i = 0; i < 50; i++)
            {
                CostTowerATKUpgrade[i] = baseCost + (i * increment);
                CostTowerATKSpeed[i] = baseCost + (i * increment);
                CostTowerATKRange[i] = baseCost + (i * increment);
            }
        }

        //체력 모두 회복
        public void FullonHP()
        {
            //Debug.Log("fullonhp spend money");
            //100원 소모시 풀피 //풀피일때 구매 막기 
            if (health != null && health.CurrentHealth != health.maxHealth && playerState.money >= CostFullHP)
            {
                health.CurrentHealth = health.maxHealth;
                playerState.SpendMoney(CostFullHP);
                UpdateButtonStates();
            }

        }

        //체력 올리기
        public void HPUpgrade()
        {
            //Debug.Log("HPUpgrade spend money");
            if (currentHPUpgradeLevel < CostHPUpgrade.Length && health != null && health.maxHealth < 1000f && playerState.money >= CostHPUpgrade[currentHPUpgradeLevel])
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
            if (health != null && currentHPTimeUpgradeLevel < IncreaseHPTimeUpgrade.Length && RgAmount < 5f && playerState.money >= CostHPTimeUpgrade[currentHPTimeUpgradeLevel])
            {

                playerState.SpendMoney(CostHPTimeUpgrade[currentHPTimeUpgradeLevel]);

                RgAmount++;
                health.HPTime(RgAmount, IncreaseHPTimeUpgrade[currentHPTimeUpgradeLevel]);
                currentHPTimeUpgradeLevel++;
                UpdateButtonStates();

            }
        }

        //자원흭득량
        public void MoneyGain()
        {
            //Debug.Log("MoneyGain spend money");
            if (currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && playerState.money >= CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel])
            {
                playerState.SpendMoney(CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("money", IncreaseMoneyGainUpgrade[currentMoneyGainUpgradeLevel]); // 증가량 적용
                currentMoneyGainUpgradeLevel++;
                //isMoneyUp = true; // MoneyGain 업그레이드 활성화
                UpdateButtonStates();
            }
        }
        public void TreeGain()
        {
            //Debug.Log("TreeGain spend money");
            if (currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && playerState.money >= CostTreeGainUpgrade[currentTreeGainUpgradeLevel])
            {
                playerState.SpendMoney(CostTreeGainUpgrade[currentTreeGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("tree", IncreaseTreeGainUpgrade[currentTreeGainUpgradeLevel]); // 증가량 적용
                currentTreeGainUpgradeLevel++;
                //isTreeUp = true; // TreeGain 업그레이드 활성화
                UpdateButtonStates();
            }
        }
        public void RockGain()
        {
            //Debug.Log("RockGain spend money");
            if (currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && playerState.money >= CostRockGainUpgrade[currentRockGainUpgradeLevel])
            {
                playerState.SpendMoney(CostRockGainUpgrade[currentRockGainUpgradeLevel]);
                ResourceManager.Instance.UpgradeResourceGain("rock", increaseRockGainUpgrade[currentRockGainUpgradeLevel]); // 증가량 적용
                currentRockGainUpgradeLevel++;
                //isRockUp = true; // RockGain 업그레이드 활성화
                UpdateButtonStates();
            }
        }

        //플레이어 이동속도업(신발모양)
        public void MoveSpeed()
        {
            //Debug.Log("MoveSpeed spend money");
            if (isMoveSpeedUp == false && playerState.money >= CostMoveSpeedUpgrade)
            {
                dynamicMove.moveSpeed = 10f;
                playerState.SpendMoney(CostMoveSpeedUpgrade);
                isMoveSpeedUp = true;
                UpdateButtonStates();
            }

        }

        //포탈 활성화
        public void PotalActivate()
        {
            //Debug.Log("PotalActivate spend money");
            if (isPotalActive == false && playerState.money >= CostPotalActivateUpgrade)
            {
                for (int i = 0; i < potalsColor.Length; i++)
                {
                    potalsColor[i].material.color = Color.red;
                    potalsEffect[i].SetActive(true);
                }
                XRSimpleInteractable xRSimpleInteractable = potal.GetComponent<XRSimpleInteractable>();
                xRSimpleInteractable.enabled = true;
                playerState.SpendMoney(CostPotalActivateUpgrade);
                isPotalActive = true;
                UpdateButtonStates();
            }

        }

        //아이템 흭득 범위 증가
        public void AutoGain()
        {
            if (isAutoGain == false && playerState.money >= CostAutoGainUpgrade)
            {
                ResourceManager.speed = 10f;
                ResourceManager.distance = 10f;
                playerState.SpendMoney(CostAutoGainUpgrade);
                isAutoGain = true;
            }
        }

        //캐슬 방어력 업그레이드
        public void ArmorUpgrade()
        {
            if (currenteArmorUpgradeLevel < CostArmorUpgrade.Length && health != null && playerState.money >= CostArmorUpgrade[currenteArmorUpgradeLevel])
            {
                health.ChangedArmor(increaseArmorUpgrade[currenteArmorUpgradeLevel]);
                playerState.SpendMoney(CostArmorUpgrade[currenteArmorUpgradeLevel]);
                currenteArmorUpgradeLevel++;
                UpdateButtonStates();
            }

        }

        //타워 공격력 업그레이드
        public void TowerATKUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);


            if (currentTowerATKUpgradeLevel < CostTowerATKUpgrade.Length && playerState.money >= (CostTowerATKUpgrade[currentTowerATKUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKUpgrade[currentTowerATKUpgradeLevel]);
                currentTowerATKUpgradeLevel++;

                //공격력 업
                if (buffContents != null)
                {
                    buffContents.atk = 0f;
                    buffContents.atk++;
                    atkLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }


                UpdateButtonStates();
            }
        }
        //타워 공격속도 업그레이드
        public void TowerATKSpeedUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            if (currentTowerATKSpeedUpgradeLevel < CostTowerATKSpeed.Length && playerState.money >= (CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]);
                currentTowerATKSpeedUpgradeLevel++;

                //타워 공격속도 업
                if (buffContents != null)
                {
                    buffContents.shootDelay = 0f;
                    buffContents.shootDelay++;
                    atkSpeedLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }
                UpdateButtonStates();
            }
        }
        //타워 공격 범위 업그레이드
        public void TowerATKRangeUpgrade()
        {
            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            if (currentTowerATKRangeUpgradeLevel < CostTowerATKRange.Length && playerState.money >= (CostTowerATKRange[currentTowerATKRangeUpgradeLevel]))
            {
                playerState.SpendMoney(CostTowerATKRange[currentTowerATKRangeUpgradeLevel]);
                currentTowerATKRangeUpgradeLevel++;
                // 공격 범위 증가 로직 추가             
                if (buffContents != null)
                {
                    buffContents.atkRange = 0f;
                    buffContents.atkRange++;
                    atkRangeLevel++;

                }
                foreach (var tower in towerbase)
                {
                    tower.BuffTower(buffContents, true);
                }
                UpdateButtonStates();
            }
        }




        //버튼 상태 업데이트
        private void UpdateButtonStates()
        {
            btnList[0].interactable = playerState.money >= CostFullHP && health.CurrentHealth != health.maxHealth; // FullHP 버튼
            btnList[1].interactable = currentHPUpgradeLevel < CostHPUpgrade.Length && health.maxHealth < 1000f && playerState.money > (CostHPUpgrade[currentHPUpgradeLevel]); // HPUpgrade 버튼
            btnList[2].interactable = currentHPTimeUpgradeLevel < CostHPTimeUpgrade.Length && RgAmount < 5f && playerState.money > CostHPTimeUpgrade[currentHPTimeUpgradeLevel]; // HPTimeUpgrade 버튼
            btnList[3].interactable = currenteArmorUpgradeLevel < CostArmorUpgrade.Length && playerState.money > CostArmorUpgrade[currenteArmorUpgradeLevel]; // 아머
            btnList[4].interactable = currentTowerATKUpgradeLevel < CostTowerATKUpgrade.Length && playerState.money >= CostTowerATKUpgrade[currentTowerATKUpgradeLevel]; // 타워 ATK 업그레이드
            btnList[5].interactable = currentTowerATKSpeedUpgradeLevel < CostTowerATKSpeed.Length && playerState.money >= CostTowerATKSpeed[currentTowerATKSpeedUpgradeLevel]; // 타워 공격 속도 업그레이드
            btnList[6].interactable = currentTowerATKRangeUpgradeLevel < CostTowerATKRange.Length && playerState.money >= CostTowerATKRange[currentTowerATKRangeUpgradeLevel]; // 타워 범위 업그레이드
            btnList[7].interactable = currentMoneyGainUpgradeLevel < CostMoneyGainUpgrade.Length && playerState.money >= CostMoneyGainUpgrade[currentMoneyGainUpgradeLevel]; // MoneyGain 버튼
            btnList[8].interactable = currentTreeGainUpgradeLevel < CostTreeGainUpgrade.Length && playerState.money >= CostTreeGainUpgrade[currentTreeGainUpgradeLevel]; // TreeGain 버튼
            btnList[9].interactable = currentRockGainUpgradeLevel < CostRockGainUpgrade.Length && playerState.money >= CostRockGainUpgrade[currentRockGainUpgradeLevel]; // RockGain 버튼
            btnList[10].interactable = playerState.money >= CostMoveSpeedUpgrade && !isMoveSpeedUp; // MoveSpeed 버튼
            btnList[11].interactable = playerState.money >= CostPotalActivateUpgrade && !isPotalActive; // PotalActivate 버튼
            btnList[12].interactable = playerState.money >= CostAutoGainUpgrade && !isAutoGain; //AutoGain 버튼
        }

        //가격 업데이트 
        void PriceUpdate()
        {
            PriceTexts[0].text = $"{CostFullHP} G";
            PriceTexts[1].text = GetUpgradePriceText(currentHPUpgradeLevel, CostHPUpgrade);
            PriceTexts[2].text = GetUpgradePriceText(currentHPTimeUpgradeLevel, CostHPTimeUpgrade);
            PriceTexts[3].text = GetUpgradePriceText(currenteArmorUpgradeLevel, CostArmorUpgrade);
            PriceTexts[4].text = GetUpgradePriceText(currentTowerATKUpgradeLevel, CostTowerATKUpgrade);
            PriceTexts[5].text = GetUpgradePriceText(currentTowerATKSpeedUpgradeLevel, CostTowerATKSpeed);
            PriceTexts[6].text = GetUpgradePriceText(currentTowerATKRangeUpgradeLevel, CostTowerATKRange);

            PriceTexts[7].text = GetUpgradePriceText(currentMoneyGainUpgradeLevel, CostMoneyGainUpgrade);
            PriceTexts[8].text = GetUpgradePriceText(currentTreeGainUpgradeLevel, CostTreeGainUpgrade);
            PriceTexts[9].text = GetUpgradePriceText(currentRockGainUpgradeLevel, CostRockGainUpgrade);
            PriceTexts[10].text = $"{CostMoveSpeedUpgrade} G";
            PriceTexts[11].text = $"{CostPotalActivateUpgrade} G";
            PriceTexts[12].text = $"{CostAutoGainUpgrade} G";
        }

        // 업그레이드 가격 텍스트를 반환하는 메서드
        private string GetUpgradePriceText(int upgradeLevel, float[] costArray)
        {
            return upgradeLevel < costArray.Length ? $"{costArray[upgradeLevel]} G" : "Max";
        }
    }
}