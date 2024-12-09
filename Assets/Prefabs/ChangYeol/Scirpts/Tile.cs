using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        [HideInInspector] public GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        public TowerInfo[] towerInfo;
        //빌드매니저 객체
        private BuildManager buildManager;
        //설치하면 생성되는 이펙트
        public GameObject TowerImpectPrefab;
        //플레이어 위치
        public BuildMenu buildMenu;
        //타워 업그레이드 여부
        public bool IsUpgrade { get; private set; }
        //플레이어 왼손 그랍 라인 비주얼
        public XRInteractorLineVisual lineVisual;
        //트리거 키 입력
        public InputActionProperty property;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
        }
        private void Update()
        {
            if (property.action.WasPressedThisFrame())
            {
                if (lineVisual.reticle)
                {
                    Destroy(lineVisual.reticle);
                    lineVisual.reticle = null;
                    buildMenu.BuildUI.SetActive(true);
                }
            }
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            /*base.OnSelectEntered(args);
            if (!buildManager.playerState.SpendMoney(towerInfo[buildMenu.indexs].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (lineVisual.reticle && buildManager.playerState.SpendMoney(towerInfo[buildMenu.indexs].cost1))
            {
                tower = Instantiate(towerInfo[buildMenu.indexs].projectile.tower, GetBuildPosition(), Quaternion.identity);

                TowerBase towerBase = tower.GetComponent<TowerBase>();
                CastleUpgrade castleUpgrade = GetComponent<CastleUpgrade>();

                if (towerBase != null)
                {
                    towerBase.BuffTower(CastleUpgrade.buffContents, true);
                }
                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                box.size = buildMenu.boxes[buildMenu.indexs].size;
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }*/
        }

        //타워 설치 위치
        public Vector3 GetBuildPosition()
        {
            if (lineVisual.reticle)
            {
                return lineVisual.reticle.transform.position;
            }
            return Vector3.zero;
        }
        //타워 생성
        public void BuildTower(Vector3 size, Vector3 center)
        {
            //설치할 터렛의 속성값 가져오기 (터렛 프리팹, 건설비용, 업그레이드 프리팹, 업그레이드 비용...)
            towerInfo[0] = buildManager.GetTowerToBuild();
            //돈을 지불한다 100, 250
            //Debug.Log($"터렛 건설비용: {blueprint.cost}");
            //lineVisual.reticle를 towerInfo에 저장한 upgradetower를 설정
            if (lineVisual.reticle)
            {
                Destroy(lineVisual.reticle);
                lineVisual.reticle = null;
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                lineVisual.reticle.GetComponent<BoxCollider>().enabled = false;
                return;
            }
            else if (!lineVisual.reticle)
            {
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                lineVisual.reticle.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}