using Defend.Tower;
using Defend.Utillity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HealthBar 의 기능 정의
/// </summary>
namespace Defend.UI
{
    public class StatusUI : MonoBehaviour
    {
        #region Variables
        public Transform target;                // Player
        public GameObject healthBar;            // 체력바 Transform
        public Image fillHealth;                // 체력바
        public TextMeshProUGUI healthText;      // 체력 텍스트
        public GameObject manaBar;              // 마나바 Transform
        public Image fillMana;                  // 마나바
        public TextMeshProUGUI manaText;        // 마나 텍스트
        public GameObject buffsFisrt;           // 버프창1
        public GameObject buffsSecond;          // 버프창2
        [SerializeField] private Status status; // 상태를 받아올 스크립트
        #endregion

        void Start()
        {
            if (target == null)
            {
                // TODO :: Player를 바라봐야함
                target = Camera.main.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + target.forward);
            // TODO :: 델리게이트 사용해서 넘기자
            SetFillHealth();
            SetHealthText();
            SetFillMana();
            SetManaText();
        }

        void SetFillHealth()
        {
            fillHealth.fillAmount = (status.CurrentHealth / status.MaxHealth);
        }
        void SetHealthText()
        {
            healthText.text = $"{Mathf.Round(status.CurrentHealth)}/{status.MaxHealth}";
        }
        void SetFillMana()
        {
            fillMana.fillAmount = (status.CurrentMana / status.MaxMana);
        }
        void SetManaText()
        {
            manaText.text = $"{Mathf.Round(status.CurrentMana)}/{status.MaxMana}";
        }
    }
}
