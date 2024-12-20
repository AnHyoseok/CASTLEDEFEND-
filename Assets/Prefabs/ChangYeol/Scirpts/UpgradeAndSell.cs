using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class UpgradeAndSell : MonoBehaviour
    {
        #region Variables
        //�Ǹ� �� ������ Ÿ���� ����â
        public GameObject PropertiesUI;
        //���׷��̵� �� ������ Ÿ���� ���׷��̵� Ÿ�� ����â
        public GameObject DescriptionUI;
        private BuildManager buildManager;
        //���ù��� Ÿ��
        private TowerXR tower;

        //�⺻ Ÿ�� ���Ű���, �ǸŰ���, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //���׷��̵� Ÿ�� ���� �ؽ�Ʈ, ��ư, �ǸŰ��� �ؽ�Ʈ
        public Upgrade upGradeText;

        //���׷��̵� ��ư
        public Button upgradebutton;
        #endregion

        void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }

        //�Ű������� ������ Ÿ�� ������ ���´�
        public void ShowTileUI(TowerXR towerXR)
        {
            //���ù��� Ÿ�� ����
            tower = towerXR;
            TowerInfo info = tower.GetComponent<TowerBase>().GetTowerInfo();
            TowerInfo upinfo = tower.upgradetowerInfo;

            //���׷��̵� ���� ǥ��
            if ((tower.Isupgradeone && tower.Isupgradetwo && tower.currentlevel == 3)|| 
                (tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3) 
                || !tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 3)
            {
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Sell : " + info.GetSellCost().ToString() + " G, tree : " + info.GetSellTreeCost().ToString() + " rock : " + info.GetSellRockCost().ToString();
                basicText.Sellcost.text = "";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.AttackRange.text = "AttackRang : " + info.projectile.attackRange.ToString();
                basicText.UpgradeMoney.text = "";
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(false);
            }
            else if((tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2) ||
                (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 2))
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + "G, tree : " + info.cost3.ToString() + " rock : " + info.cost4.ToString();
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G, tree : " + info.GetSellTreeCost().ToString() + " rock : " + info.GetSellRockCost().ToString();
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.AttackRange.text = "AttackRang : " + info.projectile.attackRange.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2 + "G, tree : " + info.cost3.ToString() + " rock : " + info.cost4.ToString();
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Upgrade : " + upinfo.cost2.ToString() + " G\n                  tree : " + upinfo.cost3.ToString() + " rock : " + upinfo.cost4.ToString(); ;
                upGradeText.Sellcost.text = "Sell : " + upinfo.GetSellCost().ToString() + " G, tree : " + upinfo.GetSellTreeCost().ToString() + " rock : " + upinfo.GetSellRockCost().ToString();
                upGradeText.Hp.text = "Hp : " + upinfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + upinfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + upinfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + upinfo.projectile.moveSpeed.ToString();
                upGradeText.AttackRange.text = "AttackRange : " + upinfo.projectile.attackRange.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
            else if (!tower.Isupgradeone && !tower.Isupgradetwo && tower.currentlevel == 1)
            {
                //�⺻ �ͷ� �Ǹ� ���� ǥ��
                basicText.image.sprite = tower.currentTower[tower.currentindex];
                basicText.name.text = buildManager.buildMenu.boxes[tower.currentindex].name;
                basicText.Buycost.text = "Buy : " + info.cost1 + " G";
                basicText.Sellcost.text = "Sell : " + info.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + info.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + info.maxMana.ToString();
                basicText.Attack.text = "Attack : " + info.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + info.projectile.moveSpeed.ToString();
                basicText.AttackRange.text = "AttackRang : " + info.projectile.attackRange.ToString();
                basicText.UpgradeMoney.text = "Upgrade : " + info.cost2 + "G, tree : " + info.cost3.ToString() + " rock : " + info.cost4.ToString();
                //���׷��̵� �Ǹ� ���� ǥ��
                upGradeText.image.sprite = tower.currentTower[tower.currentindex + 1];
                upGradeText.name.text = buildManager.buildMenu.boxes[tower.currentindex + 1].name;
                upGradeText.Buycost.text = "Upgrade : " + upinfo.cost2.ToString() + " G\n                  tree : " + upinfo.cost3.ToString() + ", rock : " + upinfo.cost4.ToString();
                upGradeText.Sellcost.text = "Sell : " + upinfo.GetSellCost().ToString() + " G, tree : " + upinfo.GetSellTreeCost().ToString() + ", rock : " + upinfo.GetSellRockCost().ToString();
                upGradeText.Hp.text = "Hp : " + upinfo.maxHealth.ToString();
                upGradeText.Mp.text = "Mp : " + upinfo.maxMana.ToString();
                upGradeText.Attack.text = "Attack : " + upinfo.projectile.attack.ToString();
                upGradeText.AttackSpeed.text = "AttackSpeed : " + upinfo.projectile.moveSpeed.ToString();
                upGradeText.AttackRange.text = "AttackRange : " + upinfo.projectile.attackRange.ToString();
                PropertiesUI.SetActive(true);
                DescriptionUI.SetActive(true);
            }
        }
        //���������� UI �Ⱥ��̰� �ϱ�
        public void HidetileUI()
        {
            PropertiesUI.SetActive(false);
            //���ù��� Ÿ�� �ʱ�ȭ
            tower = null;
        }
    }
}