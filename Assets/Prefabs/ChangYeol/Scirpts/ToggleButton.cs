using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class ToggleButton : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public bool isOnto = false;
        [HideInInspector] public bool isOnPlay = false;
        //Tunneling
        public GameObject TunnelingGameobject;
        
        public GameObject PlayerMain;
        //환경설정 창
        public GameObject Preferences;

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
            OnOffToggle();

        }
        public void OnOffToggle()
        {
            isOnto = !TunnelingGameobject.activeSelf;
            TunnelingGameobject.SetActive(isOnto);
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
            if(TunnelingGameobject && Preferences)
            {
                TunnelingGameobject.SetActive(true);
                Preferences.SetActive(false);
            }
        }
        public void OffPreferences()
        {
            if (TunnelingGameobject && Preferences)
            {
                TunnelingGameobject.SetActive(false);
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