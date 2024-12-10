using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;
namespace Defend.UI
{
    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;
        //타워들의 정보값
        //public BuildTowerUI[] buildtowerinfos;
        public TowerInfo[] towerinfo;
        public Sprite[] towerSprite;
        public BoxCollider[] boxes;
        public Button[] buttons;

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
        public void SelectTowwer(int index)
        {
            indexs = index;

            tile.BuildTower(boxes[index].size, boxes[index].center);
            BuildUI.SetActive(false);
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
            Destroy(tile.lineVisual.reticle);
            tile.lineVisual.reticle = null;
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