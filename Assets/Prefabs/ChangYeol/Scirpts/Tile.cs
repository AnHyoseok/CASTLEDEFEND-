using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        [HideInInspector]public GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        public TowerInfo[] towerInfo;
        private Image[] towerimage;

        private Vector3 offset;

        //빌드매니저 객체
        private BuildManager buildManager;
        //설치하면 생성되는 이펙트
        public GameObject TowerImpectPrefab;
        //플레이어 위치
        public Transform head;
        //타워 업그레이드 여부
        public bool IsUpgrade { get; private set; }
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
        public void BuildTower(Vector3 size, Vector3 center)
        {
            //설치할 터렛의 속성값 가져오기 (터렛 프리팹, 건설비용, 업그레이드 프리팹, 업그레이드 비용...)
            towerInfo[1] = buildManager.GetTowerToBuild();
            
            //돈을 지불한다 100, 250
            //Debug.Log($"터렛 건설비용: {blueprint.cost}");
            //타워 생성
            tower = Instantiate(towerInfo[1].upgradeTower, GetBuildPosition(), Quaternion.identity);
            //타워를 잡을 수 있는 컴포런트 추가
            tower.AddComponent<BoxCollider>();
            tower.AddComponent<TowerXR>();
            XRGrabInteractable xRGrabInter = tower.GetComponent<TowerXR>();

            BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
            boxCollider.size = size;
            boxCollider.center = center;
            //타워 생성 이펙트
            /*GameObject effgo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            //타일 자식으로 생성
            effgo.transform.parent = transform;

            Destroy(effgo, 1.5f);*/
        }
        public void SellTower()
        {
            /*//업그레이드 터렛을 판매
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //업그레이드터렛들의 반값으로 판매
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }*/
            //기본 터렛을 판매
            if (tower != null)
            {
                Destroy(tower);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                buildManager.playerState.AddMoney(1);
            }
        }

        public void UpgradeTower(Vector3 size, Vector3 center)
        {
            
            if (tower == null)
            {
                Debug.Log("업그레이드 실패했습니다");
                return;
            }
            if(tower)
            {
                towerInfo[2] = buildManager.GetTowerToBuild();
                Debug.Log("터렛 업그레이드");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //터렛 업그레이드 여부
                IsUpgrade = true;

                //터렛 업그레이드 생성
                tower_upgrade = Instantiate(towerInfo[2].upgradeTower, tower.transform.position, Quaternion.identity);
                tower_upgrade.AddComponent<BoxCollider>();
                tower_upgrade.AddComponent<TowerXR>();
                BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
                boxCollider.size = size;
                boxCollider.center = center;
                Destroy(tower);
                tower = tower_upgrade;
                tower_upgrade = null;
            }
        }
    }
}