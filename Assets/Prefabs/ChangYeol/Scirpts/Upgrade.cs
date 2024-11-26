using TMPro;
using UnityEngine;

namespace Defend.UI
{
    [System.Serializable]
    public class Upgrade
    {
        #region Variables
        //업그레이드 UI 및 업그레이드 할 타워 정보 텍스트
        public GameObject UpgradeUI;
        public TextMeshProUGUI Towername;
        public TextMeshProUGUI Towercost;
        public TextMeshProUGUI TowerHp;
        public TextMeshProUGUI TowerAttack;
        #endregion
    }
}