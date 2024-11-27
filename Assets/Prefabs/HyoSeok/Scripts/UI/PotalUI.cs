using UnityEngine;

namespace Defend.UI
{
    public class PotalUI : MonoBehaviour
    {
        #region Variables
        public GameObject potalMenu;
        public Transform player;

        #endregion

      
        public void OnPotalUI()
        {
            potalMenu.SetActive(true);
            //Debug.Log("Æ÷Å» uiÃ¢ ¶ç¿ì±â");
        }

      
    }
}