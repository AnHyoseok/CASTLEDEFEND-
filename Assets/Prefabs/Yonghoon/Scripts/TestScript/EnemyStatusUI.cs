using Defend.TestScript;
using Defend.Utillity;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HealthBar 의 기능 정의
/// </summary>
namespace Defend.UI
{
    public class EnemyStatusUI : MonoBehaviour
    {
        #region Variables
        public Transform target;              // Player
        public GameObject healthBar;          // 체력바 Transform
        public Image fillHealth;              // 체력바
        public TextMeshProUGUI healthText;    // 체력 텍스트
        public GameObject buffsFisrt;         // 버프창1
        public GameObject buffsSecond;        // 버프창2

        //참조를 받아올 변수
        private Health health;
        #endregion

        void Start()
        {
            health = GetComponentInParent<Health>();

            health.OnDamaged += SetHealthUI;
            health.OnHeal += SetHealthUI;

            //UI 초기화
            //healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.MaxHealth}";
            healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.maxHealth}";
            fillHealth.fillAmount = health.GetRatio();

            if (target == null)
            {
                // TODO :: Player를 바라봐야함
                target = Camera.main.transform;
            }
        }

        private void SetHealthUI(float amount)
        {
            healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.maxHealth}";
            fillHealth.fillAmount = health.GetRatio();
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + target.forward);
            // TODO :: 델리게이트 사용해서 넘기자
        }
    }
}
