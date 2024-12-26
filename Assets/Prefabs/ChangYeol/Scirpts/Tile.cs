using Defend.Tower;
using Defend.Utillity;
using UnityEngine;
using UnityEngine.EventSystems;
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
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        [HideInInspector] public TowerInfo towerInfo;
        //빌드매니저 객체
        private BuildManager buildManager;
        //설치하면 생성되는 이펙트
        public GameObject[] TowerImpectPrefab;
        //플레이어 위치
        public BuildMenu buildMenu;
        //플레이어 왼손 라인
        public XRRayInteractor leftRayInteractor;
        //플레이어 왼손 레티클 비주얼
        [HideInInspector]public XRInteractorReticleVisual leftReticleVisual;
        //설치할 타워를 보여주는 게임 오브젝트
        [SerializeField] public GameObject reticlePrefabs;
        //트리거 키 입력
        public InputActionProperty property;
        public InputActionProperty leftSelect;
        //타워 선택이 가능한 레이어 설정
        public InteractionLayerMask layerMask;
        //타워 설치 위치
        private Vector3 leftHitPoint;
        // 터레인
        public GameObject terrain;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
            leftReticleVisual = leftRayInteractor.GetComponent<XRInteractorReticleVisual>();
            //rightReticleVisual = rightRayInteractor.GetComponent<XRInteractorReticleVisual>();
        }
        private void Update()
        {
            //Trigger 버튼 누르면 reticle = null
            if (property.action.WasPressedThisFrame())
            {
                leftReticleVisual.reticlePrefab = null;
                //buildMenu.istrigger = false;
                return;
            }
            /*if (leftSelect.action.WasPressedThisFrame() && leftReticleVisual.enabled == true)
            {
                SetBuildTower();
            }*/
            if (leftReticleVisual == null/* || rightReticleVisual == null*/) return;

            // Ray가 UI를 향하고 있는 경우
            if (leftRayInteractor.TryGetCurrentUIRaycastResult(out RaycastResult result))
            {
                leftReticleVisual.enabled = false;
            }
            else
            {
                IsBuildTower();
            }
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //타워 설치
            SetBuildTower();
        }
        /*// 라인의 유효성을 검사하는 함수
        private bool IsLineVisualValid()
        {
            // XRRayInteractor의 히트 정보를 가져옴
            if (leftRayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Vector3 hitNormal,
                out int hitIndex, out bool isValidTarget))
            {
                return isValidTarget; // 타겟이 유효한지 반환
            }
            return false;
        }*/
        //타워 설치 위치
        private Vector3 GetBuildPosition()
        {
            return leftHitPoint;
        }
        //타워 설치 위치를 보여준다
        private void IsBuildTower()
        {
            if (leftRayInteractor == null || leftReticleVisual == null)
                return;

            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lefthit))
            {
                // 현재 레이캐스트 히트 지점 가져오기
                leftHitPoint = lefthit.point;

                // Terrain인지 확인
                if (lefthit.collider.gameObject == terrain)
                {
                    leftReticleVisual.enabled = true;
                }
                else
                {
                    leftReticleVisual.enabled = false;
                }
            }
        }
        //타워 설치
        private void SetBuildTower()
        {
            if (leftReticleVisual.reticlePrefab == null) return;
            if(leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lefthit))
            {
                leftHitPoint = lefthit.point;
                TowerXR tower = lefthit.collider.gameObject.GetComponent<TowerXR>();
                if (tower != null)
                {
                    return;
                }
            }
            if (buildManager.playerState.SpendMoney(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().cost1) 
                //&& buildMenu.isReticle 
                && buildMenu.towerinfo[buildMenu.indexs].isLock)
            {
                tower = Instantiate(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().projectile.tower,
                    GetBuildPosition(), Quaternion.identity);
                GameObject effgo = Instantiate(TowerImpectPrefab[0], tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);
                tower.transform.parent = buildManager.transform;
                //효과음 재생
                AudioUtility.CreateSFX(buildManager.towerBuildSound, tower.transform.position, AudioUtility.AudioGroups.EFFECT, 1);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                TowerXR towerXR = tower.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.isTrigger = false;
                box.size = buildMenu.boxes[buildMenu.indexs].size + new Vector3(0.5f, 0, 0.5f);
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }
        }
    }
}