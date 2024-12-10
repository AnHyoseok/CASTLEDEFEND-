using Defend.Player;
using Defend.Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
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
        public TowerInfo towerInfo;
        //빌드매니저 객체
        private BuildManager buildManager;
        //설치하면 생성되는 이펙트
        public GameObject TowerImpectPrefab;
        //플레이어 위치
        public BuildMenu buildMenu;
        //타워 업그레이드 여부
        public bool IsUpgrade { get; private set; }
        //플레이어 왼손 그랍 라인 비주얼
        public XRRayInteractor rayInteractor;
        public XRInteractorReticleVisual reticleVisual;
        public XRInteractorLineVisual lineVisual;
        private GameObject reticlePrefab;
        //트리거 키 입력
        public InputActionProperty property;
        public InteractionLayerMask layerMask;
        private Vector3 hitPoint;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
            reticleVisual.reticlePrefab = null;
        }
        private void Update()
        {
            /*if (property.action.WasPressedThisFrame())
            {
                if (lineVisual.reticle)
                {
                    Destroy(lineVisual.reticle);
                    lineVisual.reticle = null;
                    buildMenu.BuildUI.SetActive(true);
                }
            }*/
            if (rayInteractor == null) return;

            // 현재 레이캐스트 히트 지점 가져오기
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                hitPoint = hit.point; // Reticle이 표시하는 위치
            }
            if (rayInteractor == null || reticleVisual == null)
                return;

            // 라인이 유효한지 확인
            if (IsLineVisualValid())
            {
                // 허용된 경우 ReticlePrefab 활성화
                if (reticleVisual.reticlePrefab == null)
                {
                    reticleVisual.reticlePrefab = reticlePrefab;
                    Debug.Log("ReticlePrefab 활성화");
                }
            }
            else
            {
                // 허용되지 않은 경우 ReticlePrefab 비활성화
                if (reticleVisual.reticlePrefab != null)
                {
                    reticleVisual.reticlePrefab = null;
                    Debug.Log("ReticlePrefab 비활성화");
                }
            }
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            if (!buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (buildManager.playerState.SpendMoney(buildMenu.towerinfo[buildMenu.indexs].cost1))
            {
                buildMenu.towerinfo[buildMenu.indexs].projectile.attack += CastleUpgrade.buffContents.atk;

                tower = Instantiate(buildMenu.towerinfo[buildMenu.indexs].projectile.tower, GetBuildPosition(), Quaternion.identity);
                
                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                TowerXR towerXR = tower.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.size = buildMenu.boxes[buildMenu.indexs].size;
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
                buildMenu.buildpro.SetActive(false);
            }
        }
        private bool IsLineVisualValid()
        {
            if (rayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Vector3 hitNormal, out int hitIndex, out bool isValidTarget))
            {
                return isValidTarget;
            }
            return false;
        }
        //타워 설치 위치
        public Vector3 GetBuildPosition()
        {
            return hitPoint;
        }
        //타워 생성
        public void BuildTower(Vector3 size, Vector3 center)
        {
            //lineVisual.reticle를 towerInfo에 저장한 upgradetower를 설정
            //XRRayInteractor interactor = lineVisual.GetComponent<XRRayInteractor>();
            Debug.Log($"ㅇㅅㅇ");
            GameObject towerFakePrefab = buildMenu.falsetowers[buildMenu.indexs];
            Debug.Log($"{towerFakePrefab.name}");
            GameObject game = Instantiate(towerFakePrefab);
            reticleVisual.reticlePrefab = towerFakePrefab;
            reticleVisual.reticlePrefab.GetComponent<BoxCollider>().enabled = false;
            /*if (reticleVisual.reticlePrefab)
            {
                Destroy(reticleVisual.reticlePrefab);
                reticleVisual.reticlePrefab = null;
                reticleVisual.reticlePrefab = buildMenu.falsetowers[buildMenu.indexs];

                reticleVisual.reticlePrefab.GetComponent<BoxCollider>().enabled = false;
                return;
            }
            else if (!reticleVisual.reticlePrefab)
            {
                //lineVisual.reticle = towerInfo[0].projectile.tower;
                //lineVisual.reticle = buildMenu.falsetowers[buildMenu.indexs];
                reticleVisual.reticlePrefab = buildMenu.falsetowers[buildMenu.indexs];
                reticleVisual.reticlePrefab.GetComponent<BoxCollider>().enabled = false;
            }*/
        }
    }
}