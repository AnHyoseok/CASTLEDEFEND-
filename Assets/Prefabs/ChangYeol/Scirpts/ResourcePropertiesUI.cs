using Defend.Player;
using UnityEngine;
using static Defend.Interactive.Resources;

namespace Defend.UI
{
    public class ResourcePropertiesUI : MonoBehaviour
    {
        #region Variables
        public GameObject ResourceProUI;

        public Upgrade ResourceText;

        public Transform head;

        public ResourceType resources;
        #endregion

        private void Start()
        {
            ResourceText.name.text = "Tree";
            ResourceText.Hp.text = "Hp : " + resources.health.ToString();
        }
    }
}
