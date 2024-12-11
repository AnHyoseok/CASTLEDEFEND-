using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRSimpleInteractable
    {
        #region Variables
        public TowerInfo[] towerInfo = new TowerInfo[12];

        private GameObject tower_upgrade;

        //빌드매니저 객체
        private BuildManager buildManager;

        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
            for (int i = 0; i < towerInfo.Length; i++)
            {
                towerInfo[i] = buildManager.buildMenu.towerinfo[i];
                towerInfo[i].upgradeTower = buildManager.buildMenu.towerinfo[i].upgradeTower;
                towerInfo[i].projectile.tower = buildManager.buildMenu.towerinfo[i].projectile.tower;
                towerInfo[i].projectile = buildManager.buildMenu.towerinfo[i].projectile;
            }
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            buildManager.buildMenu.isReticle = false;
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            buildManager.buildMenu.isReticle = true;
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTower(this);
        }
        public void SellTower()
        {
            //기본 터렛을 판매
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                buildManager.playerState.AddMoney(towerInfo[buildManager.buildMenu.indexs].GetSellCost());
                buildManager.DeselectTile();
            }
            else if (!this.gameObject)
            {
                Debug.Log("판매하지 못했습니다");
            }
        }

        public void UpgradeTower()
        {
            if (towerInfo[buildManager.buildMenu.indexs] == null)
            {
                Debug.Log("업그레이드 실패했습니다");
                return;
            }
            if (towerInfo[buildManager.buildMenu.indexs] != null)
            {
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);
                if (towerInfo[buildManager.buildMenu.indexs].upgradeTower &&
                    buildManager.playerState.SpendMoney(towerInfo[buildManager.buildMenu.indexs].cost2))
                {
                    //터렛 업그레이드 여부
                    buildManager.IsUpgrade = true;
                    //터렛 업그레이드 생성
                    tower_upgrade = Instantiate(towerInfo[buildManager.buildMenu.indexs].upgradeTower,
                        transform.position, Quaternion.identity);
                    tower_upgrade.AddComponent<BoxCollider>();
                    tower_upgrade.AddComponent<TowerXR>();
                    BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                    boxCollider.size = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].size;
                    boxCollider.center = buildManager.buildMenu.boxes[buildManager.buildMenu.indexs].center;
                    Destroy(this.gameObject);
                    tower_upgrade = null;
                    buildManager.DeselectTile();
                    buildManager.buildMenu.levelindex += 1;
                    buildManager.buildMenu.istowerup = false;
                }
            }
        }
    }
}