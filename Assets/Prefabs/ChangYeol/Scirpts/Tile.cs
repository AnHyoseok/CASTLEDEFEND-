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
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector] public GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;
        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public BuildMenu buildMenu;
        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }
        //�÷��̾� �޼� �׶� ���� ���־�
        public XRRayInteractor rayInteractor;
        public XRInteractorReticleVisual reticleVisual;
        public XRInteractorLineVisual lineVisual;
        private GameObject reticlePrefab;
        //Ʈ���� Ű �Է�
        public InputActionProperty property;
        public InteractionLayerMask layerMask;
        private Vector3 hitPoint;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
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

            // ���� ����ĳ��Ʈ ��Ʈ ���� ��������
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                hitPoint = hit.point; // Reticle�� ǥ���ϴ� ��ġ
            }
            if (rayInteractor == null || reticleVisual == null)
                return;

            // ������ ��ȿ���� Ȯ��
            if (IsLineVisualValid())
            {
                // ���� ��� ReticlePrefab Ȱ��ȭ
                if (reticleVisual.reticlePrefab == null)
                {
                    reticleVisual.reticlePrefab = reticlePrefab;
                    Debug.Log("ReticlePrefab Ȱ��ȭ");
                }
            }
            else
            {
                // ������ ���� ��� ReticlePrefab ��Ȱ��ȭ
                if (reticleVisual.reticlePrefab != null)
                {
                    reticleVisual.reticlePrefab = null;
                    Debug.Log("ReticlePrefab ��Ȱ��ȭ");
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
        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return hitPoint;
        }
        //Ÿ�� ����
        public void BuildTower(Vector3 size, Vector3 center)
        {
            //lineVisual.reticle�� towerInfo�� ������ upgradetower�� ����
            //XRRayInteractor interactor = lineVisual.GetComponent<XRRayInteractor>();
            Debug.Log($"������");
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