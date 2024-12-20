using Defend.Player;
using Defend.Tower;
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
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            for (int i = 0; i < buildManager.buildMenu.towerSprite.Length; i++)
            {
                currentTower[i] = buildManager.buildMenu.towerSprite[i];
            }
            //����
            towerBase = GetComponent<TowerBase>();
            towerInfo = towerBase.GetTowerInfo();
            CastleUpgrade castle = buildManager.buildMenu.GetComponent<CastleUpgrade>();
            towerInfo.projectile.attack += castle.atkLevel;
            towerInfo.projectile.moveSpeed += castle.atkSpeedLevel;
            towerInfo.projectile.attackRange += castle.atkRangeLevel;
            if (towerBase.GetTowerInfo().upgradeTower)
            {
                upgradetowerInfo = towerBase.GetTowerInfo().upgradeTower.GetComponent<TowerBase>().GetTowerInfo();
                upgradetowerInfo.projectile.attack += castle.atkLevel;
                upgradetowerInfo.projectile.moveSpeed += castle.atkSpeedLevel;
                upgradetowerInfo.projectile.attack += castle.atkRangeLevel;
            }
        }
        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            base.OnHoverEntering(args);
            //buildManager.buildMenu.isReticle = false;
            buildManager.buildMenu.tile.reticleVisual.enabled = false;
        }
        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);
            //if (!buildManager.buildMenu.istrigger) return;
            //buildManager.buildMenu.isReticle = true;
            buildManager.buildMenu.tile.reticleVisual.enabled = true;
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTower(this);
        }
        public void SellTower()
        {
            //�⺻ �ͷ��� �Ǹ�
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
                GameObject effect = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[3], transform.position, Quaternion.identity);
                Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                buildManager.playerState.AddMoney(towerInfo.GetSellCost());
                buildManager.playerState.AddRock(towerInfo.GetSellRockCost());
                buildManager.playerState.AddTree(towerInfo.GetSellTreeCost());
                buildManager.DeselectTile();
            }
            else if (!this.gameObject)
            {
                Debug.Log("�Ǹ����� ���߽��ϴ�");
            }
        }
        public void UpgradeTower()
        {
            if (towerInfo == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (towerInfo != null)
            {
                if (towerInfo.upgradeTower &&
                    buildManager.playerState.SpendMoney(towerInfo.cost2) && buildManager.playerState.SpendResources())
                {
                    if (currentlevel == 1 && !Isupgradeone)
                    {
                        //Effect
                        GameObject effectGo = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[1], transform.position, Quaternion.identity);
                        Destroy(effectGo, 2f);
                        //�ͷ� ���׷��̵� ����
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
                        GameObject effectGo = Instantiate(buildManager.buildMenu.tile.TowerImpectPrefab[2], transform.position, Quaternion.identity);
                        Destroy(effectGo, 2f);
                        //�ͷ� ���׷��̵� ����
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