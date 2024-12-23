using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class ToggleButton : MonoBehaviour
    {
        #region Variables
        public bool isOnPlay = false;
        public bool isOnto = false;
        //Tunneling
        public GameObject TunnelingGameobject;
        
        public GameObject PlayerMain;
        //환경설정 창
        public GameObject Preferences;
        public GameObject OperationManual;
        public InputActionProperty property;
        #endregion
        private void Update()
        {
            if(property.action.WasPressedThisFrame())
            {
                if(Preferences)
                {
                    Preferences.SetActive(!Preferences.activeSelf);
                }
            }
        }
        public void OnOffToggle()
        {
            isOnto = !isOnto;
            if (!isOnto)
            {
                Ontoggle();
            }
            else
            {
                Offtoggle();
            }
        }
        public void OnOffPlayer()
        {
            isOnPlay = !isOnPlay;
            if (!isOnPlay)
            {
                OnPlayerMine();
            }
            else
            {
                OffPlayerMine();
            }
        }
        void Ontoggle()
        {
            TunnelingGameobject.SetActive(true);
        }
        void Offtoggle()
        {
            TunnelingGameobject.SetActive(false);
        }
        public void OnPreferences()
        {
            if(OperationManual && Preferences)
            {
                OperationManual.SetActive(true);
                Preferences.SetActive(false);
            }
        }
        public void OffPreferences()
        {
            if (OperationManual && Preferences)
            {
                OperationManual.SetActive(false);
                Preferences.SetActive(true);
            }
        }
        void OnPlayerMine()
        {
            PlayerMain.SetActive(true);
        }
        void OffPlayerMine()
        {
            PlayerMain.SetActive(false);
        }
    }
}