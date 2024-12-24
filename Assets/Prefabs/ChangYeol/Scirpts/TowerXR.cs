using Defend.Player;
using Defend.Tower;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRSimpleInteractable
    {
        #region Variables
        [SerializeField]private TowerInfo towerInfo;
        public TowerInfo upgradetowerInfo;
        public int currentindex = BuildManager.instance.buildMenu.indexs;
        public Sprite[] currentTower = new Sprite[24];
        private GameObject tower_upgrade;
        private TowerBase towerBase;
        public int currentlevel = BuildManager.instance.buildMenu.levelindex;
        public bool Isupgradeone;
        public bool Isupgradetwo;
        //빌드매니저 객체
        private BuildManager buildManager;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            //초기화
            buildManager = BuildManager.Instance;
            for (int i = 0; i < buildManager.buildMenu.towerSprite.Length; i++)
            {
                currentTower[i] = buildManager.buildMenu.towerSprite[i];
            }
            //참조
            towerBase = GetComponent<TowerBase>();
            towerInfo = towerBase.GetTowerInfo();
            CastleUpgrade castle = buildManager.buildMenu.GetComponent<CastleUpgrade>();
            towerInfo.projectile.attack += (1 * castle.atkLevel);
            for (int i = 0;i < castle.atkSpeedLevel; i++)
            {
                towerInfo.shootDelay *= (0.99f);
            }
            for (int i = 0; i< castle.atkRangeLevel; i++)
            {
                towerInfo.attackRange *= (1.01f);
            }
        }
        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            base.OnHoverEntering(args);
            //buildManager.buildMenu.isReticle = false;
            buildManager.buildMenu.tile.leftReticleVisual.enabled = false;
        }
        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);
            //if (!buildManager.buildMenu.istrigger) return;
            //buildManager.buildMenu.isReticle = true;
            buildManager.buildMenu.tile.leftReticleVisual.enabled = true;
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTower(this);
        }
        public void SellTower()
        {
            if (this.gameObject != gameObject) return;
            //기본 터렛을 판매
            if (this.gameObject != null && this.gameObject == buildManager.tower.gameObject)
            {
                Destroy(this.gameObject);
                GameObject effect = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[3],
                    transform.position, Quaternion.identity);
                Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                buildManager.playerState.AddMoney(towerInfo.GetSellCost());
                buildManager.playerState.AddRock(towerInfo.GetSellRockCost());
                buildManager.playerState.AddTree(towerInfo.GetSellTreeCost());
                buildManager.DeselectTile();
            }
            else if (!this.gameObject)
            {
                Debug.Log("판매하지 못했습니다");
            }
        }
        public void UpgradeTower()
        {
            if (towerInfo == null || this.gameObject != buildManager.tower.gameObject) return;
            if (towerInfo != null)
            {
                if (towerInfo.upgradeTower &&
                    buildManager.playerState.SpendMoney(towerInfo.cost2)
                    && buildManager.playerState.SpendResources(towerInfo.cost3,towerInfo.cost4))
                {
                    AudioUtility.CreateSFX(buildManager.towerBuildSound, transform.position, AudioUtility.AudioGroups.EFFECT);
                    if (currentlevel == 1 && !Isupgradeone)
                    {
                        //Effect
                        GameObject effectGo = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[1],
                            transform.position, Quaternion.identity);
                        Destroy(effectGo, 2f);
                        //터렛 업그레이드 생성
                        tower_upgrade = Instantiate(towerInfo.upgradeTower, transform.position, Quaternion.identity);
                        tower_upgrade.AddComponent<BoxCollider>();
                        tower_upgrade.AddComponent<TowerXR>();
                        TowerXR tower = tower_upgrade.GetComponent<TowerXR>();
                        tower.currentindex = currentindex + 1;
                        tower.currentlevel = currentlevel + 1;
                        tower.Isupgradeone = true;
                        BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                        boxCollider.size = buildManager.buildMenu.boxes[currentindex].size + new Vector3(0.5f, 0, 0.5f);
                        boxCollider.center = buildManager.buildMenu.boxes[currentindex].center;
                        Destroy(this.gameObject);
                        tower_upgrade = null;
                        buildManager.DeselectTile();
                    }
                    else if (currentlevel == 2 && Isupgradeone)
                    {
                        //Effect
                        GameObject effectGo = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[2],
                            transform.position, Quaternion.identity);
                        Destroy(effectGo, 2f);
                        //터렛 업그레이드 생성
                        tower_upgrade = Instantiate(towerInfo.upgradeTower, transform.position, Quaternion.identity);
                        tower_upgrade.AddComponent<BoxCollider>();
                        tower_upgrade.AddComponent<TowerXR>();
                        TowerXR tower = tower_upgrade.GetComponent<TowerXR>();
                        tower.currentindex = currentindex + 1;
                        tower.currentlevel = currentlevel + 1;
                        tower.Isupgradeone = true;
                        tower.Isupgradetwo = true;
                        BoxCollider boxCollider = tower_upgrade.GetComponent<BoxCollider>();
                        boxCollider.size = buildManager.buildMenu.boxes[currentindex].size + new Vector3(0.5f, 0, 0.5f);
                        boxCollider.center = buildManager.buildMenu.boxes[currentindex].center;
                        Destroy(this.gameObject);
                        tower_upgrade = null;
                        buildManager.DeselectTile();
                    }
                }
            }
        }
    }
}