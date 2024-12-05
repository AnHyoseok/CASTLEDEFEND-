using Defend.Player;
using Defend.Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        public GameObject[] towers;
        private GameObject tower;
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
        public BuildMenu buildMenu;
        //타워 업그레이드 여부
        public bool IsUpgrade { get; private set; }
        [SerializeField] private float distance = 1.5f;

        public XRInteractorLineVisual lineVisual;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            tower = Instantiate(lineVisual.reticle, GetBuildPosition(), Quaternion.identity);
            Destroy(lineVisual.reticle);
            lineVisual.reticle = null;
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
        }
        //타워 설치 위치
        public Vector3 GetBuildPosition()
        {
            Debug.Log("towerselectssss");
            return lineVisual.reticle.transform.position;
        }
        //타워 생성
        public void BuildTower(Vector3 size, Vector3 center, int index)
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
                lineVisual.reticle = towerInfo[0].projectile.tower;
                return;
            }
            else if(!lineVisual.reticle)
            {
                lineVisual.reticle = towerInfo[0].projectile.tower;
            }
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
                towerInfo[0] = buildManager.GetTowerToBuild();
                Debug.Log("터렛 업그레이드");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //터렛 업그레이드 여부
                IsUpgrade = true;

                //터렛 업그레이드 생성   
                tower_upgrade = Instantiate(towerInfo[0].upgradeTower, tower.transform.position, Quaternion.identity);
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