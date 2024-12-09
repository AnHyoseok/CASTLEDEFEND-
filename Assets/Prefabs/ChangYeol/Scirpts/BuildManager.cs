using Defend.Player;
using Defend.Tower;
using Unity.VisualScripting;
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
        private TowerInfo towerInfo;
        private Sprite TowerSprite;

        //선택한 타일이 있는지, 선택하지 안했으면 건설 못한다
        public bool CannotBuild => towerInfo == null;

        public PlayerState playerState;

        public WarningWindow warningWindow;

        public Button Sellbutton;
        public Button UpgradeButton;

        public bool IsUpgrade;

        //선택한 타워의 건설한 비용을 자지고 있는지
        public bool HasBuildMoney
        {
            get
            {
                if (towerInfo == null)
                    return false;
                //나중에 타워 설치 비용
                return playerState.SpendMoney(towerInfo.cost2);
            }
        }

        //타일 UI
        public UpgradeAndSell menu;
        public BuildMenu buildMenu;
        //선택한 타워
        private TowerXR tower;

        /*//선택한 적
        public EnemyState enemyStats;
        //적 속성 UI
        public EnemyPropertiesUI EnemyproUI;
        private EnemyXRSimple xRSimple;*/
        #endregion

        public TowerInfo GetTowerToBuild()
        {
            return towerInfo;
        }
        //매개변수로 받은 타워 프리팹을 설치할 타워에 저장
        public void SetTowerToBuild(TowerInfo Tower)
        {
            towerInfo = Tower;
            /*TowerSprite = sprite;*/
            DeselectTile();
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
        /*public EnemyState GetEnemyToBuild()
        {
            return enemyStats;
        }
        public void SetEnemyToBuild(EnemyState enemy)
        {
            enemyStats = enemy;
            DeselectEnemy();
        }*/
        /*public void SelectEnemy(EnemyXRSimple enemy)
        {
            //같은 적을 선택하면 HideUI
            if (enemy == xRSimple)
            {
                DeselectTile();
                return;
            }

            //선택한 적 저장하기
            enemy = xRSimple;
            //저장한 적 속성을 초기화
            enemyStats = null;
            EnemyproUI.ShowProUI(enemy);
        }
        public void DeselectEnemy()
        {
            EnemyproUI.HideProUI();

            xRSimple = null;
        }*/
    }
}
