using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HpBar 의 기능 정의
/// </summary>
namespace Defend.UI
{
    public class HpBar : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform target;
        [SerializeField] private Image fillHp;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private GameObject owner;

        #endregion

        void Start()
        {
            if (target == null)
            {
                target = Camera.main.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + target.forward);
            SetFiilAmount();
            SetHpText();
        }

        void SetFiilAmount()
        {
            fillHp.fillAmount = 0.5f;
        }
        void SetHpText()
        {
            hpText.text = $"{50}/{100}";
        }
    }
}
