using TMPro;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerHandUI : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI timeText;
        private float time;
        #endregion

        private void Update()
        {
            time += Time.deltaTime;
            timeText.text = time.ToString(); 
        }
    }
}