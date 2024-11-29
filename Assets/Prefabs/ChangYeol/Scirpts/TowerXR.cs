using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRGrabInteractable
    {
        #region Variables
        public TowerInfo towerInfo;

        //빌드매니저 객체
        private BuildManager buildManager;

        XRGrabInteractable xRGrab;

        BoxCollider boxCollider;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            xRGrab = this.GetComponent<XRGrabInteractable>();
            boxCollider = this.GetComponent<BoxCollider>();
            xRGrab.colliders.Add(boxCollider);
            boxCollider.size = new Vector3(1, 2.516554f, 1);
            boxCollider.center = new Vector3(0, 0.2562535f, 0);
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            buildManager.DeselectTile();
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            buildManager.SelectTile(this);
        }
        public void SellTower()
        {
            /*//업그레이드 터렛을 판매
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //업그레이드터렛들의 반값으로 판매
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }*/
            //기본 터렛을 판매
            if (this != null)
            {
                Destroy(this);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                //PlayerState.AddMoney();
            }
        }

        public void UpgradeTower()
        {
            Debug.Log("터렛 업그레이드");
            /*if (blueprint == null)
            {
                //Debug.Log("업그레이드 실패했습니다");
                return;
            }
            if (PlayerStats.UseMoney(blueprint.costUpgrade))
            {
                //Effect
                GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //터렛 업그레이드 여부
                IsUpgrade = true;

                //터렛 업그레이드 생성
                turret_upgrade = Instantiate(TowerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
                Destroy(turret);
            }*/
        }
    }
}