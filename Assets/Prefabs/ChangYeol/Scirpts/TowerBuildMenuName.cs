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

        public BuildMenu build;
        private BuildManager buildManager;
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
        private void Update()
        {
            StartBuild();
            UnLockUpdate();
        }
        void StartBuild()
        {
            for (int i = 0; i < towerinfo.Length; i++)
            {
                towerBuildButtons[i].interactable = build.towerinfo[i * 3].isLock;

                float Attack = buildManager.towerBases[i * 3].GetTowerInfo().projectile.attack;
                Attack += castleUpgrade.atkLevel;
                float Range = buildManager.towerBases[i * 3].GetTowerInfo().attackRange;
                Range += castleUpgrade.atkRangeLevel;

                //타워 빌드창
                towerinfo[i].image.sprite = build.towerSprite[i * 3];
                towerinfo[i].Hp.text = "Hp : " + buildManager.towerBases[i * 3].GetTowerInfo().maxHealth.ToString();
                towerinfo[i].Mp.text = "Mp : " + buildManager.towerBases[i * 3].GetTowerInfo().maxMana.ToString();
                towerinfo[i].Attack.text = "Attack : " + Attack.ToString();
                towerinfo[i].AttackSpeed.text = "Range : " + Range.ToString();
                towerinfo[i].Buycost.text = " : " + buildManager.towerBases[i * 3].GetTowerInfo().cost1.ToString();
            }
            for (int i = 1; i < towerinfo.Length; i++)
            {
                float Attack = buildManager.towerBases[i * 3].GetTowerInfo().projectile.attack;
                Attack += castleUpgrade.atkLevel;
                float Range = buildManager.towerBases[i * 3].GetTowerInfo().attackRange;
                Range += castleUpgrade.atkRangeLevel;
                //타워 구매창
                buyTower[i - 1].image.sprite = build.towerSprite[i * 3];
                buyTower[i - 1].Hp.text = "Hp : " + buildManager.towerBases[i * 3].GetTowerInfo().maxHealth.ToString();
                buyTower[i - 1].Mp.text = "Mp : " + buildManager.towerBases[i * 3].GetTowerInfo().maxMana.ToString();
                buyTower[i - 1].Attack.text = "Attack : " + Attack.ToString();
                buyTower[i - 1].AttackSpeed.text = "Range : " + Range.ToString();
                buyTower[i - 1].Buycost.text = " : 100";
            }
        }
        /*public void SelectTower()
        {
            selectTower.SetActive(true);
            selectTowerinfo.image.sprite = build.towerSprite[build.indexs];
            selectTowerinfo.name.text = towerinfo[build.indexs].name.text;
            selectTowerinfo.Hp.text = "Hp : " + buildManager.towerBases[build.indexs].GetTowerInfo().maxHealth.ToString();
            selectTowerinfo.Mp.text = "Mp : " + buildManager.towerBases[build.indexs].GetTowerInfo().maxMana.ToString();
            selectTowerinfo.Attack.text = "Attack : " + buildManager.towerBases[build.indexs].GetTowerInfo().projectile.attack.ToString();
            selectTowerinfo.AttackSpeed.text = "Armor : " + buildManager.towerBases[build.indexs].GetTowerInfo().armor.ToString();
        }*/
        public void UnLockTower(int index)
        {
            if (build.towerinfo[index * 3].isLock) return;
            build.indexs = index;
            //Debug.Log(index);
            if (BuildManager.instance.playerState.SpendMoney(100) && BuildManager.instance.playerState.SpendResources())
            {
                towerBuildButtons[index].interactable = false;

                build.towerinfo[index * 3].isLock = true;
                towerBuildButtons[index].interactable = build.towerinfo[index * 3].isLock;
                unlockTowerButton[index-1].interactable = !build.towerinfo[index * 3].isLock;
            }
        }
        void UnLockUpdate()
        {
            for (int i = 1; i < towerBuildButtons.Length; i++)
            {
                towerBuildButtons[i].interactable = build.towerinfo[i * 3].isLock;
                unlockTowerButton[i-1].interactable = !build.towerinfo[i * 3].isLock;
            }
        }
    }
}