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
        public GameObject healthBar;            // ü�¹� Transform
        public Image fillHealth;                // ü�¹�
        public TextMeshProUGUI healthText;      // ü�� �ؽ�Ʈ
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
            // target ��ġ���� ���� ������Ʈ�� ��ġ�� �� ���� ���͸� ���
            Vector3 direction = target.position - transform.position;

            // UI�� Z���� �ƴ϶� �ٸ� ��(��: Y��)���� �ٶ󺸰� ����
            Quaternion rotation = Quaternion.LookRotation(-direction); // Z�� ������ ���� ���� ���
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