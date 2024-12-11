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

            //업그레이드 가격 표시
            if (buildManager.buildMenu.levelindex == 3 && (buildManager.buildMenu.indexs + 1) % 3 == 0)
            {
                //최종 타워 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "";
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if (buildManager.buildMenu.levelindex == 1 || (buildManager.buildMenu.levelindex == 2 && !buildManager.IsUpgrade))
            {
                //기본 터렛 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2;
                //업그레이드 판매 가격 표시
                upGradeText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].name;
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if (buildManager.buildMenu.levelindex == 2 && buildManager.IsUpgrade)
            {
                //기본 터렛 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                basicText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2;
                //업그레이드 판매 가격 표시
                upGradeText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 2];
                upGradeText.name.text = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 2].name;
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 2].projectile.moveSpeed.ToString();
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