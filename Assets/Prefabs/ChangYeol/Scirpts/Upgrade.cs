using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    [System.Serializable]
    public class Upgrade
    {
        #region Variables
        //업그레이드 UI 및 업그레이드 할 타워 정보 텍스트
        public Image image;
        public TextMeshProUGUI name;
        public TextMeshProUGUI Buycost;
        public TextMeshProUGUI Sellcost;
        public TextMeshProUGUI Hp;
        public TextMeshProUGUI Mp;
        public TextMeshProUGUI Attack;
        public TextMeshProUGUI AttackSpeed;
        public TextMeshProUGUI AttackRange;
        public TextMeshProUGUI UpgradeMoney;
        #endregion
    }
}