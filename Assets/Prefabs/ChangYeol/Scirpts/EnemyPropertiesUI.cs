using Defend.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class EnemyPropertiesUI : MonoBehaviour
    {
        #region Variables
        public GameObject EnemyProUI;

        public Upgrade EnemyText;

        public Transform head;

        private BuildManager buildManager;
        #endregion

        private void Start()
        {
            buildManager = BuildManager.Instance;
            
        }
        public void ShowProUI( )
        {
            
        }
        public void HideProUI()
        {
            EnemyProUI.SetActive(false);
        }
    }
}