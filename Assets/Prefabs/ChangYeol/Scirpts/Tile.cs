using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector] public GameObject tower;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        [HideInInspector] public TowerInfo towerInfo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //�÷��̾� �� ����
        public XRRayInteractor rayInteractor;
        //�÷��̾� �޼� ��ƼŬ ���־�
        [HideInInspector]public XRInteractorReticleVisual reticleVisual;
        //��ġ�� Ÿ���� �����ִ� ���� ������Ʈ
        [SerializeField] public GameObject reticlePrefabs;
        //Ʈ���� Ű �Է�
        public InputActionProperty property;
        //Ÿ�� ������ ������ ���̾� ����
        public InteractionLayerMask layerMask;
        //Ÿ�� ��ġ ��ġ
        private Vector3 hitPoint;
        // �ͷ���
        public GameObject terrain;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            reticleVisual = rayInteractor.GetComponent<XRInteractorReticleVisual>();
        }
        private void Update()
        {
            //Trigger ��ư ������ reticle = null
            if (property.action.WasPressedThisFrame())
            {
                reticleVisual.reticlePrefab = null;
                //buildMenu.istrigger = false;
                return;
            }

            if (rayInteractor == null) return;

            IsBuildTower();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //Ÿ�� ��ġ
            SetBuildTower();
        }
        // ������ ��ȿ���� �˻��ϴ� �Լ�
        private bool IsLineVisualValid()
        {
            // XRRayInteractor�� ��Ʈ ������ ������
            if (rayInteractor.TryGetHitInfo(out Vector3 hitPosition, out Vector3 hitNormal,
                out int hitIndex, out bool isValidTarget))
            {
                return isValidTarget; // Ÿ���� ��ȿ���� ��ȯ
            }
            return false;
        }
        // ReticlePrefab�� �����ϴ� �Լ�
        //private void SetReticlePrefab()
        //{
        //    reticlePrefabs = buildMenu.falsetowers[buildMenu.indexs];
        //    reticlePrefabs.GetComponent<BoxCollider>().enabled = false;
        //    reticleVisual.reticlePrefab = reticlePrefabs;
        //}
        //Ÿ�� ��ġ ��ġ
        private Vector3 GetBuildPosition()
        {
            return hitPoint;
        }
        //Ÿ�� ��ġ ��ġ�� �����ش�
        private void IsBuildTower()
        {
            if (rayInteractor == null || reticleVisual == null)
                return;

            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                // �浹�� ������Ʈ�� ���� ��������
                GameObject hitObject = hit.collider.gameObject;
                // ���� ����ĳ��Ʈ ��Ʈ ���� ��������
                hitPoint = hit.point;
                if (hitObject == terrain)
                {
                    reticleVisual.enabled = true;
                }
                else
                {
                    reticleVisual.enabled = false;
                }
            }
            // ������ ��ȿ���� Ȯ��
            //if (IsLineVisualValid())
            //{
            //    if (!buildMenu.istrigger || !buildMenu.isReticle)
            //    {
            //        reticleVisual.reticlePrefab = null;
            //        reticlePrefabs = null;
            //        return;
            //    }
            //    // ���� ��� ReticlePrefab Ȱ��ȭ�� �Ǿ��� ��ġ
            //    if (reticleVisual.reticlePrefab == null && buildMenu.isReticle )
            //    {
            //        SetReticlePrefab();
            //        if(reticleVisual.reticlePrefab != null)
            //        {
            //            rayInteractor.uiHoverEntered.AddListener(UIEnterReticle);
            //            rayInteractor.uiHoverExited.AddListener(UIExitReticle);
            //        }
            //    }
            //}
            //else
            //{
            //    // ������ ���� ��� ReticlePrefab ��Ȱ��ȭ
            //    if (reticleVisual.reticlePrefab != null && buildMenu.isReticle)
            //    {
            //        reticleVisual.reticlePrefab = null;
            //    }
            //}
        }
        void UIEnterReticle(UIHoverEventArgs args)
        {
            Debug.Log("ENTER UI");
            reticleVisual.enabled = false;
        }
        void UIExitReticle(UIHoverEventArgs uIHover)
        {
            Debug.Log("EXIT UI");
            //if (!buildMenu.istrigger) return;
            reticleVisual.enabled = true;
        }
        //Ÿ�� ��ġ
        private void SetBuildTower()
        {
            //if (!buildMenu.istrigger) return;
            if (buildManager.playerState.SpendMoney(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().cost1) 
                //&& buildMenu.isReticle 
                && buildMenu.towerinfo[buildMenu.indexs].isLock)
            {
                tower = Instantiate(buildManager.towerBases[buildMenu.indexs].GetTowerInfo().projectile.tower,
                    GetBuildPosition(), Quaternion.identity);

                GameObject effgo = Instantiate(TowerImpectPrefab, tower.transform.position, Quaternion.identity);
                Destroy(effgo, 2f);

                tower.AddComponent<BoxCollider>();
                tower.AddComponent<TowerXR>();
                BoxCollider box = tower.GetComponent<BoxCollider>();
                TowerXR towerXR = tower.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.isTrigger = false;
                box.size = buildMenu.boxes[buildMenu.indexs].size;
                box.size = box.size + new Vector3(0.5f, 0, 0.5f);
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
                buildMenu.buildpro.SetActive(false);
            }
        }
        
    }
}