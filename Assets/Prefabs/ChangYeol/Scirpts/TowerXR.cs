using Defend.Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRSimpleInteractable
    {
        #region Variables
        public TowerInfo[] towerInfo = new TowerInfo[12];

        //ºôµå¸Å´ÏÀú °´Ã¼
        private BuildManager buildManager;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            for (int i = 0; i < towerInfo.Length; i++)
            {
                towerInfo[i] = buildManager.buildMenu.towerinfo[i];
                towerInfo[i].upgradeTower = buildManager.buildMenu.towerinfo[i].upgradeTower;
                towerInfo[i].projectile.tower = buildManager.buildMenu.towerinfo[i].projectile.tower;
                towerInfo[i].projectile = buildManager.buildMenu.towerinfo[i].projectile;
            }
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            //buildManager.DeselectTile();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTile(this);
        }
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log("ddd");
        }
    }
}