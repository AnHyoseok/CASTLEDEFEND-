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
        public TowerInfo[] towerInfo;

        //ºôµå¸Å´ÏÀú °´Ã¼
        private BuildManager buildManager;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
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

        protected override void OnActivated(ActivateEventArgs args)
        {
            base.OnActivated(args);
            buildManager.SelectTile(this);
        }
        public void OnActionUI()
        {
            Debug.Log("act");
            buildManager.SelectTile(this);
        }
    }
}