using UnityEngine;

namespace Defend.UI
{
    public class TowerBuildMenuName : MonoBehaviour
    {
        #region Variables
        public Upgrade[] towerinfo;
        public Upgrade selectTowerinfo;

        public BuildMenu build;
        public GameObject selectTower;
        #endregion
        private void Start()
        {
            StartBuild();
        }
        public void StartBuild()
        {
            for (int i = 0; i < towerinfo.Length; i++)
            {
                towerinfo[i].image.sprite = build.towerSprite[i];
                towerinfo[i].Hp.text = "Hp : " + build.towerinfo[i].maxHealth.ToString();
                towerinfo[i].Mp.text = "Mp : " + build.towerinfo[i].maxMana.ToString();
                towerinfo[i].Attack.text = "Attack : " + build.towerinfo[i].projectile.attack.ToString();
                towerinfo[i].AttackSpeed.text = "Armor : " + build.towerinfo[i].armor.ToString();
                towerinfo[i].Buycost.text = "Buy Money : " + build.towerinfo[i].cost1.ToString();
            }
        }
        public void Stst(int index)
        {
            selectTower.SetActive(true);
            selectTowerinfo.image.sprite = build.towerSprite[index];
            selectTowerinfo.name.text = towerinfo[index].name.text;
            selectTowerinfo.Hp.text = "Hp : " + build.towerinfo[index].maxHealth.ToString();
            selectTowerinfo.Mp.text = "Mp : " + build.towerinfo[index].maxMana.ToString();
            selectTowerinfo.Attack.text = "Attack : " + build.towerinfo[index].projectile.attack.ToString();
            selectTowerinfo.AttackSpeed.text = "Armor : " + build.towerinfo[index].armor.ToString();
        }
    }
}