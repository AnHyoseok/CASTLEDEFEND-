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

        //ºôµå¸Å´ÏÀú °´Ã¼
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
            buildManager.SelectTile(this);
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            buildManager.DeselectTile();
        }
    }
}