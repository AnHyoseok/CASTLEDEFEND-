using UnityEngine;

namespace Defend.UI
{
    public class ToggleButton : MonoBehaviour
    {
        #region Variables
        public bool isOn = false;

        public GameObject toggleGameobject;
        #endregion
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
    }
}