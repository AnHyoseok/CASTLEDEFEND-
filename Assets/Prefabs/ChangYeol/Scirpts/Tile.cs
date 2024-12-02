using Defend.Tower;
using UnityEngine;

namespace Defend.UI
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        [HideInInspector]public GameObject tower;
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        [HideInInspector]public TowerInfo towerInfo;

        private Vector3 offset;

        //빌드매니저 객체
        private BuildManager buildManager;
        //설치하면 생성되는 이펙트
        public GameObject TowerImpectPrefab;
        //플레이어 위치
        public Transform head;
        //타워 업그레이드 여부
        //public bool IsUpgrade { get; private set; }
        [SerializeField] private float distance = 1.5f;
        #endregion

        private void Start()
        {
            //초기화

            buildManager = BuildManager.Instance;
        }
        //타워 설치 위치
        public Vector3 GetBuildPosition()
        {
            return head.position + new Vector3(head.forward.x,0,head.forward.z).normalized * distance;
        }
        //타워 생성
        public void BuildTower()
        {
            if (towerInfo == null)
            {
                buildManager.warningWindow.ShowWarning("You have not selected a build");
            }

            //설치할 터렛의 속성값 가져오기 (터렛 프리팹, 건설비용, 업그레이드 프리팹, 업그레이드 비용...)
            towerInfo = buildManager.GetTowerToBuild();

            //돈을 지불한다 100, 250
            //Debug.Log($"터렛 건설비용: {blueprint.cost}");
            //타워 생성
            tower = Instantiate(towerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
            //타워를 잡을 수 있는 컴포런트 추가
            tower.AddComponent<BoxCollider>();
            tower.AddComponent<TowerXR>();
            //타워 생성 이펙트
            GameObject effgo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            //타일 자식으로 생성
            effgo.transform.parent = transform;

            Destroy(effgo, 1.5f);
        }
    }
}