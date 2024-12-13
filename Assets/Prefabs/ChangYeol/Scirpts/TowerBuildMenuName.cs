using Defend.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class TowerBuildMenuName : MonoBehaviour
    {
        #region Variables
        [SerializeField]private Upgrade[] towerinfo;
        [SerializeField]private Upgrade[] buyTower;
        [SerializeField]private Upgrade selectTowerinfo;

        public BuildMenu build;
        private BuildManager buildManager;
        public GameObject selectTower;
        public Button[] towerBuildButtons;
        public Button[] unlockTowerButton;
        private CastleUpgrade castleUpgrade;
        #endregion
        private void Start()
        {
            //참조
            castleUpgrade = GetComponent<CastleUpgrade>();
            //초기화
            buildManager = BuildManager.Instance;
            StartBuild();
        }
        void StartBuild()
        {
            for (int i = 0; i < towerinfo.Length; i++)
            {
                towerBuildButtons[i].interactable = build.towerinfo[i].isLock;
                //buildManager.towerBases[i].GetTowerInfo().projectile.attack += castleUpgrade.atkLevel;
                //타워 빌드창
                towerinfo[i].Hp.text = "Hp : " + buildManager.towerBases[i].GetTowerInfo().maxHealth.ToString();
                towerinfo[i].Mp.text = "Mp : " + buildManager.towerBases[i].GetTowerInfo().maxMana.ToString();
                towerinfo[i].Attack.text = "Attack : " + buildManager.towerBases[i].GetTowerInfo().projectile.attack.ToString();
                towerinfo[i].AttackSpeed.text = "Armor : " + buildManager.towerBases[i].GetTowerInfo().armor.ToString();
                towerinfo[i].Buycost.text = "Buy Money : " + buildManager.towerBases[i].GetTowerInfo().cost1.ToString();
            }
            for (int i = 1; i < buildManager.towerBases.Length; i++)
            {
                //타워 구매창
                buyTower[i - 1].Hp.text = "Hp : " + buildManager.towerBases[i].GetTowerInfo().maxHealth.ToString();
                buyTower[i - 1].Mp.text = "Mp : " + buildManager.towerBases[i].GetTowerInfo().maxMana.ToString();
                buyTower[i - 1].Attack.text = "Attack : " + buildManager.towerBases[i].GetTowerInfo().projectile.attack.ToString();
                buyTower[i - 1].AttackSpeed.text = "Armor : " + buildManager.towerBases[i].GetTowerInfo().armor.ToString();
                buyTower[i - 1].Buycost.text = "Buy Money : " + buildManager.towerBases[i].GetTowerInfo().cost1.ToString();
            }
        }
        public void SelectTower()
        {
            selectTower.SetActive(true);
            selectTowerinfo.image.sprite = build.towerSprite[build.indexs];
            selectTowerinfo.name.text = towerinfo[build.indexs].name.text;
            selectTowerinfo.Hp.text = "Hp : " + buildManager.towerBases[build.indexs].GetTowerInfo().maxHealth.ToString();
            selectTowerinfo.Mp.text = "Mp : " + buildManager.towerBases[build.indexs].GetTowerInfo().maxMana.ToString();
            selectTowerinfo.Attack.text = "Attack : " + buildManager.towerBases[build.indexs].GetTowerInfo().projectile.attack.ToString();
            selectTowerinfo.AttackSpeed.text = "Armor : " + buildManager.towerBases[build.indexs].GetTowerInfo().armor.ToString();
        }
        public void UnLockTower(int index)
        {
            if (build.towerinfo[index].isLock) return;
            build.indexs = index;
            Debug.Log(index);
            if (BuildManager.instance.playerState.SpendMoney(100) && BuildManager.instance.playerState.SpendResources())
            {
                build.towerinfo[index].isLock = true;
                towerBuildButtons[index].interactable = build.towerinfo[index].isLock;
                unlockTowerButton[index-1].interactable = !build.towerinfo[index].isLock;
            }
        }
    }
}