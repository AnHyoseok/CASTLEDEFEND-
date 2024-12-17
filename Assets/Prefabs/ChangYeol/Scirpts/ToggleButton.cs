using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class ToggleButton : MonoBehaviour
    {
        #region Variables
        public bool isOn = false;

        public GameObject toggleGameobject;
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
        }
        public void OnOffToggle()
        {
            isOn = !isOn;
            if (!isOn)
            {
                Ontoggle();
            }
            else
            {
                Offtoggle();
            }
        }
        void Ontoggle()
        {
            toggleGameobject.SetActive(true);
        }
        void Offtoggle()
        {
            toggleGameobject.SetActive(false);
        }
        public void OnPreferences()
        {
            if(toggleGameobject && Preferences)
            {
                toggleGameobject.SetActive(true);
                Preferences.SetActive(false);
            }
        }
        public void OffPreferences()
        {
            if (toggleGameobject && Preferences)
            {
                toggleGameobject.SetActive(false);
                Preferences.SetActive(true);
            }
        }
    }
}