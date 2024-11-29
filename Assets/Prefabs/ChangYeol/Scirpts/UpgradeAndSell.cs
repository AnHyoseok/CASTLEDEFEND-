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
        public GameObject tileUI;

        private BuildManager buildManager;
        private TowerInfo towerInfo;
        //선택받은 타일
        private TowerXR tower;
        //private Tile targetTile;

        //기본 타워 구매가격, 판매가격, HP,MP, Attack, AttackSpeed
        public Upgrade basicText;
        //업그레이드 타워 가격 텍스트, 버튼, 판매가격 텍스트
        //public Upgrade upGradeText;

        //업그레이드 버튼
        public Button upgradebutton;
        public Button upgradeUIbutton;

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
        /*public void ShowTileUI(Tile tile)
        {
            //선택받은 타일 저장
            targetTile = tile;

            //타워가 설치된 위치 주위에서 보여준다
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            //업그레이드 가격 표시
            if (!targetTile)
            {
                //업그레이드 판매 가격 표시
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                //upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else if(targetTile)
            {
                //기본 터렛 판매 가격 표시
                basicText.name.text = targetTile.towerInfo.upgradeTower.name;
                basicText.Buycost.text = "Buy : " +  targetTile.towerInfo.cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + targetTile.towerInfo.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + targetTile.towerInfo.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + targetTile.towerInfo.maxMana.ToString();
                basicText.Attack.text = "Attack : " + targetTile.towerInfo.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + targetTile.towerInfo.projectile.moveSpeed.ToString();
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }*/
        public void ShowTileUI(TowerXR towerXR)
        {
            //선택받은 타일 저장
            tower = towerXR;

            //타워가 설치된 위치 주위에서 보여준다
            this.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0, head.forward.z).normalized * distance;
            this.transform.LookAt(new Vector3(head.position.x, this.transform.position.y, head.position.z));
            this.transform.forward *= -1;
            //업그레이드 가격 표시
            if (!tower)
            {
                //업그레이드 판매 가격 표시
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                //upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else if (tower)
            {
                //기본 터렛 판매 가격 표시
                basicText.name.text = tower.towerInfo.upgradeTower.name;
                basicText.Buycost.text = "Buy : " + tower.towerInfo.cost2.ToString() + " G";
                basicText.Sellcost.text = "Sell : " + tower.towerInfo.GetSellCost().ToString() + " G";
                basicText.Hp.text = "Hp : " + tower.towerInfo.maxHealth.ToString();
                basicText.Mp.text = "Mp : " + tower.towerInfo.maxMana.ToString();
                basicText.Attack.text = "Attack : " + tower.towerInfo.projectile.attack.ToString();
                basicText.AttackSpeed.text = "AttackSpeed : " + tower.towerInfo.projectile.moveSpeed.ToString();
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }
        //선택해제시 UI 안보이게 하기
        public void HidetileUI()
        {
            tileUI.SetActive(false);
            //선택받은 타일 초기화
            tower = null;
        }

        public void Selled()
        {
            tower.SellTower();
            buildManager.DeselectTile();
        }
        public void Upgraded()
        {
            tower.UpgradeTower();
            buildManager.DeselectTile();
        }
    }
}