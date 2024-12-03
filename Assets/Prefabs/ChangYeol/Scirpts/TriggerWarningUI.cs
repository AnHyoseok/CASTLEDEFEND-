using UnityEngine;

namespace Defend.UI
{
    public class TriggerWarningUI : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;

        public BuildMenu buildMenu;

        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other && !buildManager.isInstall)
            {
                buildManager.isSelect = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            buildManager.isInstall = true;
            if (other && buildManager.isInstall)
            {
                buildManager.isSelect = false;
            }
        }
    }
}