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
            // target 위치에서 현재 오브젝트의 위치를 뺀 방향 벡터를 계산
            Vector3 direction = target.position - transform.position;

            // UI가 Z축이 아니라 다른 축(예: Y축)으로 바라보게 설정
            Quaternion rotation = Quaternion.LookRotation(-direction); // Z축 반전을 위해 음수 사용
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