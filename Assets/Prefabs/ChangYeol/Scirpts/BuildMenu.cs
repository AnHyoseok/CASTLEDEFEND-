using Defend.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;
        //타워들의 정보값
        public BuildTowerUI Ballista;
        public BuildTowerUI Bat;
        public BuildTowerUI Cannon;
        public BuildTowerUI Crossbow;

        public GameObject[] falsetowers;

        public Tile tile;

        public GameObject BuildUI;
        public GameObject buildpro;
        public int indexs;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
        }


        //기본 터렛 버튼을 클릭시 호출
        public void SelectBallista(int index)
        {
            indexs = index;
            buildManager.SetTowerToBuild(Ballista.towerInfos[index]);
            tile.BuildTower(Ballista.towerBoxCollider[index].size, Ballista.towerBoxCollider[index].center);
            BuildUI.SetActive(false);
        }
        public void SelectBat(int index)
        {
            indexs = index;
            buildManager.SetTowerToBuild(Bat.towerInfos[index]);
            tile.BuildTower(Bat.towerBoxCollider[index].size, Bat.towerBoxCollider[index].center);
            BuildUI.SetActive(false);
        }
        public void SelectCannon(int index)
        {
            indexs = index;
            buildManager.SetTowerToBuild(Cannon.towerInfos[index]);
            tile.BuildTower(Cannon.towerBoxCollider[index].size, Cannon.towerBoxCollider[index].center);
            BuildUI.SetActive(false);
        }
        public void SelectCrossbow(int index)
        {
            indexs = index;
            buildManager.SetTowerToBuild(Crossbow.towerInfos[index]);
            tile.BuildTower(Crossbow.towerBoxCollider[index].size, Crossbow.towerBoxCollider[index].center);
            BuildUI.SetActive(false);
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
            Destroy(tile.lineVisual.reticle);
            tile.lineVisual.reticle = null;
        }
        IEnumerator TriggerWarning(TowerInfo tower)
        {
            buildManager.warningWindow.ShowWarning($"There's an {tower.upgradeTower.ToString()} in front of me");
            yield return new WaitForSeconds(2);
            buildManager.warningWindow.ShowWarning($"Create it somewhere else");
            yield return new WaitForSeconds(5);
        }
    }
}
/*
0 - BallistaTower_1
1 - BallistaTower_2
2 - BallistaTower_3
3 - BatTower_1
4 - BatTower_2
5 - BatTower_3
6 - CannonTower_1
7 - CannonTower_2
8 - CannonTower_3
9 - CrossbowTower_1
10 - CrossbowTower_2
11 - CrossbowTower_3
*/