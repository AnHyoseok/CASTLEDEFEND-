using Defend.Enemy;
using Defend.Player;
using Defend.Tower;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        //타일에 설치할 타일의 정보(프리팹, 가격정보)
        [HideInInspector] public TowerInfo towerInfo;
        //플레이어
        public PlayerState playerState;
        //경고창
        public WarningWindow warningWindow;
        //판매 버튼
        public Button Sellbutton;
        //업그레이드 버튼
        public Button UpgradeButton;
        //타일 UI
        public UpgradeAndSell menu;
        public BuildMenu buildMenu;
        //선택한 타워
        [HideInInspector] public TowerXR tower;
        //타워 안에 있는 속성값
        public TowerBase[] towerBases;
        [HideInInspector] public EnemyPropertiesUI enemy;

        public AudioClip towerBuildSound;
        #endregion
        private void Start()
        {
            enemy = menu.GetComponent<EnemyPropertiesUI>();
        }
        public void SelectTower(TowerXR towerXR)
        {
            //같은 타워를 선택하면 HideUI
            if (towerXR == tower)
            {
                DeselectTile();
                return;
            }
            //선택한 타워에 저장하기
            tower = towerXR;
            //저장한 타워 속성을 초기화
            towerInfo = null;
            //Debug.Log("타일 UI 보여주기");
            menu.ShowTileUI(towerXR);
            UpgradeButton.onClick.AddListener(towerXR.UpgradeTower);
            Sellbutton.onClick.AddListener(towerXR.SellTower);
        }
        //선택 해제
        public void DeselectTile()
        {
            //Debug.Log("타일 UI 감추기");
            menu.HidetileUI();
            UpgradeButton.onClick.RemoveAllListeners();
            Sellbutton.onClick.RemoveAllListeners();
            //선택한 타일 초기화하기
            tower = null;
        }
    }
}
