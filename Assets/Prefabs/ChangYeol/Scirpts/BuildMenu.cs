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
        public TowerInfo[] towerinfo;
        //타워들의 이미지
        public Sprite[] towerSprite;
        //타워들의 박스콜라이더
        public BoxCollider[] boxes;
        //설치 위치를 보여주는 가짜 타워
        public GameObject[] falsetowers;
        public Tile tile;
        //빌드 메뉴 UI
        public GameObject BuildUI;
        //빌드 메뉴에 타워 선택시 선택한 타워의 정보를 보여주는 UI
        public GameObject buildpro;
        //index번째 타워를 선택하면 저장하는 값 
        public int indexs;
        public int levelindex = Mathf.Clamp(1,1,3);
        //reticle이 활성화 비활성화 유무
        public bool isReticle = false;
        public bool istowerup = false;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
        }
        
        //타워 버튼을 클릭시 호출
        public void SelectTower(int index)
        {
            if (!towerinfo[index].isLock) return;
            indexs = index;
            isReticle = true;
            istowerup = true;
            BuildUI.SetActive(false);
            //buildpro.SetActive(true);
        }
        public void SetLevel(int level)
        {
            levelindex = level;
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
            isReticle=false;
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