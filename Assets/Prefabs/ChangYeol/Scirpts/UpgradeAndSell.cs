using Defend.Tower;
using TMPro;
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
        private Tile targetTile;

        //업그레이드 가격 텍스트, 버튼, 판매가격 텍스트
        public TextMeshProUGUI upgradecost;

        //업그레이드 버튼
        public Button upgradebutton;
        //판매 버튼
        public TextMeshProUGUI sellcost;

        //플레이어 카메라 위치
        public Transform head;
        #endregion

        void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;


        }

        //매개변수로 선택한 타일 정보를 얻어온다
        public void ShowTileUI(Tile tile)
        {
            //선택받은 타일 저장
            targetTile = tile;

            //타워가 설치된 위치 주위에서 보여준다
            this.transform.position = targetTile.GetBuildPosition();

            //업그레이드 가격 표시
            if (targetTile)
            {
                //업그레이드 판매 가격 표시
                //sellcost.text = targetTile.blueprint.Getupgradecost().ToString() + " G";
                upgradecost.text = "Done";
                upgradebutton.interactable = false;
            }
            else
            {
                //기본 터렛 판매 가격 표시
                sellcost.text = targetTile.towerInfo.GetSellCost().ToString() + " G";
                upgradecost.text = targetTile.towerInfo.cost2.ToString() + " G";
                upgradebutton.interactable = true;
            }
            tileUI.SetActive(true);
        }
        //선택해제시 UI 안보이게 하기
        public void HidetileUI()
        {
            tileUI.SetActive(false);
            //선택받은 타일 초기화
            targetTile = null;
        }

        public void Selled()
        {
            targetTile.SellTower();
        }
        public void Upgraded()
        {
            targetTile.UpgradeTower();
        }
    }
}