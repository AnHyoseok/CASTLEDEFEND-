using UnityEngine;

namespace Defend.UI
{
    public class TriggerWarningUI : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;

        public BuildMenu buildMenu;

        public bool isInstall;
        #endregion
        private void Start()
        {
            buildManager = BuildManager.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            TowerXR tower = other.gameObject.GetComponent<TowerXR>();
            if (tower)
            {
                buildMenu.IsSelect = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            TowerXR tower = other.gameObject.GetComponent<TowerXR>();
            if (tower)
            {
                buildMenu.IsSelect = false;
            }
        }
    }
}