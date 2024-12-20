using Defend.TestScript;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
namespace Defend.UI
{
    public class CastleHpBar : MonoBehaviour
    {
        #region Variables
        Health health;
        public Transform target;                // Player
        public GameObject castle;
        public GameObject healthBar;            // 체력바 Transform
        public Image fillHealth;                // 체력바
        public TextMeshProUGUI healthText;      // 체력 텍스트
        #endregion

        private void Start()
        {
            health = castle.GetComponent<Health>();

            if (target == null)
            {
                target = FindFirstObjectByType<XROrigin>().gameObject.transform;
            }
        }

        void Update()
        {
           
            SetFillHealth();
            SetHealthText();
           
            Vector3 direction = target.position - transform.position;

           
            Quaternion rotation = Quaternion.LookRotation(-direction); 
            transform.rotation = rotation;
        }

        void SetFillHealth()
        {
            fillHealth.fillAmount = (health.CurrentHealth / health.maxHealth);
        }
        void SetHealthText()
        {
            healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.maxHealth}";
        }
    }
}