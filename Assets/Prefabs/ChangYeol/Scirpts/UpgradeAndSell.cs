using Defend.Tower;
using MyFps;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class UpgradeAndSell : MonoBehaviour
    {
        #region Variables
        public GameObject PropertiesUI;
        public GameObject DescriptionUI;

        private BuildManager buildManager;
        private TowerInfo towerInfo;
        //선택받은 타일
        private TowerXR tower;
        public Tile tile;

        //기본 타워 구매가격, 판매가격, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //업그레이드 타워 가격 텍스트, 버튼, 판매가격 텍스트
        public Upgrade upGradeText;

        //업그레이드 버튼
        public Button upgradebutton;

        //플레이어 카메라 위치
        public Transform head;
        [SerializeField] private float distance = 1.5f;
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
            //타워가 설치된 위치 주위에서 보여준다
            this.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;

            //업그레이드 가격 표시
            /*if (tower && tile.IsUpgrade)
            {
                //기본 터렛 판매 가격 표시
                basicText.name.text = tower.towerInfo[2].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[2].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[2].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[2].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[2].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[2].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[2].projectile.moveSpeed.ToString();
            }*/
            /*if (tower && tile.IsUpgrade)
            {
                //기본 터렛 판매 가격 표시
                basicText.name.text = tower.towerInfo[1].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[1].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[1].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[1].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[1].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[1].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[1].projectile.moveSpeed.ToString();
                //업그레이드 판매 가격 표시
                upGradeText.name.text = tower.towerInfo[2].projectile.tower.ToString();
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[2].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[2].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[2].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[2].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[2].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[2].projectile.moveSpeed.ToString();
            }*/
            /*if (tower && !tile.IsUpgrade)
            {
                //기본 터렛 판매 가격 표시
                basicText.name.text = tower.towerInfo[0].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[0].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[0].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[0].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[0].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[0].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[0].projectile.moveSpeed.ToString();
                //업그레이드 판매 가격 표시
                upGradeText.name.text = tower.towerInfo[1].projectile.tower.ToString();
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[1].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[1].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[1].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[1].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[1].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[1].projectile.moveSpeed.ToString();
            }*/
            if(tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower == tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower || tile.IsUpgrade)
            {
                //업그레이드 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            if(tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower != tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower && !tile.IsUpgrade)
            {
                //기본 터렛 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs];
                basicText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].projectile.tower.ToString();
                basicText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs].cost1 + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs].GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs].maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs].projectile.moveSpeed.ToString();
                //업그레이드 판매 가격 표시
                basicText.image.sprite = buildManager.buildMenu.towerSprite[buildManager.buildMenu.indexs + 1];
                upGradeText.name.text = tower.towerInfo[buildManager.buildMenu.indexs].upgradeTower.ToString();
                upGradeText.Buycost.text = "Buy : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].cost2.ToString() + " G";
                upGradeText.Sellcost.text = "Sell : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].GetSellCost().ToString() + " G";
                upGradeText.Hp.text = "Hp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo[buildManager.buildMenu.indexs + 1].projectile.moveSpeed.ToString();
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
        public void Selled()
        {
            tile.SellTower();
            buildManager.DeselectTile();
        }
        public void Upgraded()
        {
            if ((buildManager.buildMenu.indexs + 1) % 3 == 0)
                return;
            tile.UpgradeTower(buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].size, buildManager.buildMenu.boxes[buildManager.buildMenu.indexs + 1].center);
            buildManager.DeselectTile();
        }
    }
}