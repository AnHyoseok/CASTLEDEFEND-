using Defend.Player;
using Defend.Tower;
using UnityEngine;

namespace Defend.UI
{
    public class BuildManager : MonoBehaviour
    {
        #region Singleton
        public static BuildManager instance;
        public static BuildManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Variables
        //타일에 설치할 터렛의 정보(프리팹, 가격정보)
        private TowerInfo towerInfo;

        //선택한 터렛이 있는지, 선택하지 안했으면 건설 못한다
        public bool CannotBuild => towerInfo == null;

        //선택한 터렛의 건설한 비용을 자지고 있는지
        public bool HasBuildMoney
        {
            get
            {
                if (towerInfo == null)
                    return false;

                return true;
            }

        }

        //타일 UI
        public TileUI tileUI;
        //선택한 타일
        private TileUI selectTile;
        #endregion

        public TowerInfo GetTurretToBuild()
        {
            return towerInfo;
        }

        //매개변수로 받은 터렛 프리팹을 설치할 터렛에 저장
        public void SetTurretToBuild(TowerInfo turret)
        {
            towerInfo = turret;
            DeselectTile();
        }
        //매개변수로 선택한 타일 정보를 얻어온다
        public void SelectTile(TileUI tile)
        {
            //같은 타일을 선택하면 HideUI
            if (tile == selectTile)
            {
                DeselectTile();
                return;
            }

            //선택한 타일 저장하기
            selectTile = tile;
            //저장한 터렛 속성을 초기화
            towerInfo = null;
            //Debug.Log("타일 UI 보여주기");
            //tileUI.ShowTileUI(tile);
        }
        //선택 해제
        public void DeselectTile()
        {
            //Debug.Log("타일 UI 감추기");
            //tileUI.HidetileUI();
            //선택한 타일 초기화하기
            selectTile = null;
        }
    }
}
