using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class UpgradeAndSell : MonoBehaviour
    {
        #region Variables
        //판매 및 선택한 타워의 정보창
        public GameObject PropertiesUI;
        //업그레이드 및 선택한 타워의 업그레이드 타워 정보창
        public GameObject DescriptionUI;
        private BuildManager buildManager;
        //선택받은 타일
        private TowerXR tower;

        //기본 타워 구매가격, 판매가격, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //업그레이드 타워 가격 텍스트, 버튼, 판매가격 텍스트
        public Upgrade upGradeText;

        //업그레이드 버튼
        public Button upgradebutton;
        #endregion

        void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
        }

        //매개변수로 선택한 타일 정보를 얻어온다
        public void ShowTileUI(TowerXR towerXR)
        {
            //선택받은 타워 저장
            tower = towerXR;
            TowerInfo info = tower.GetComponent<TowerBase>().GetTowerInfo();

            //업그레이드 가격 표시
            if ((tower.Isupgradeone && tower.Isupgradetwo && tower.currentlevel == 3)|| 
                (tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3) 
                || !tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3)
            {
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Sell : " + info.cost2.ToString() + " G";
                basicText.Sellcost.text = "";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "";
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if((tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2) ||
                (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2))
            {
                //기본 터렛 판매 가격 표시
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + " G";
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2;
                //업그레이드 판매 가격 표시
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.upgradetowerInfo.cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.upgradetowerInfo.GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.upgradetowerInfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.upgradetowerInfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.upgradetowerInfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.upgradetowerInfo.projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 1)
            {
                //기본 터렛 판매 가격 표시
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + " G";
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2;
                //업그레이드 판매 가격 표시
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.upgradetowerInfo.cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.upgradetowerInfo.GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.upgradetowerInfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.upgradetowerInfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.upgradetowerInfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.upgradetowerInfo.projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
        }
        //선택해제시 UI 안보이게 하기
        public void HidetileUI()
        {
            PropertiesUI.SetActive(false);
            //선택받은 타일 초기화
            tower = null;
        }
    }
}