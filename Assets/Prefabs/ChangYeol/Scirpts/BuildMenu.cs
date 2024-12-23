using Defend.Tower;
using UnityEngine;
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
        //index번째 타워를 선택하면 저장하는 값 
        public int indexs;
        public int levelindex = Mathf.Clamp(1,1,3);
        //reticle이 활성화 비활성화 유무
        //public bool isReticle = false;
        //public bool istrigger = false;
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
            // 해당 프리팹의 위치를 플레이어가 볼 수 없도록 조정
            falsetowers[indexs].transform.position = new Vector3(0, -1000, 0);
            // recticlePrefab에 선택한 타워 할당
            tile.leftReticleVisual.reticlePrefab = falsetowers[indexs];
            //isReticle = true;
            //istrigger = true;
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
            //isReticle=false;
        }
    }
}