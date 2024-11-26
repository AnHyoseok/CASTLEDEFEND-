using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TileUI : XRSimpleInteractable
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        public GameObject tower;
        private GameObject tower_upgrade;
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;

        //빌드매니저 객체
        private BuildManager buildManager;

        //렌더러 인스턴스
        private Renderer rend;

        //마우스가 위에 있을때 타일 컬러값
        //public Color hoverColor;
        //맵타일의 기본 Color
        public Color notenoughColor;

        //마우스가 위에 있을때 타일 메터리얼
        public Material hoverMaterial;
        //맵타일의 기본 Material
        private Material startMaterial;

        //이펙트 프리팹
        public GameObject TowerImpectPrefab;

        //타워 업그레이드 여부
        public bool IsUpgrade { get; private set; }

        //판매 이펙트 프리팹
        public GameObject SellImpectPrefab;
        #endregion

        private void Start()
        {
            //초기화
            buildManager = BuildManager.Instance;

            //rend = this.transform.GetComponent<Renderer>();
            rend = this.GetComponent<Renderer>();
            //startColor = rend.material.color;
            startMaterial = rend.material;
            IsUpgrade = false;
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            Debug.Log("설치할 곳");
            /*if (buildManager.CannotBuild)
            {
                return;
            }
            rend.enabled = true;
            //rend.material.color = hoverColor;
            rend.material = hoverMaterial;

            //선택한 터렛을 건설한 비용을 가지고 있는지 잔고확인
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }
            if (tower != null || tower_upgrade != null)
            {
                rend.material.color = notenoughColor;
                return;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            Debug.Log("설치");
            /*if (tower != null || tower_upgrade != null)
            {
                buildManager.SelectTile(this);
                return;
            }
            if (buildManager.CannotBuild)
            {
                Debug.Log("터렛을 설치하지 못했습니다"); //터렛을 선택하지 않은 상태
                return;
            }
            BuildTurret();*/
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            Debug.Log("설치X");
            /*rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;*/
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Debug.Log("설치X");
            /*rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;*/
        }
        /*private void OnMouseEnter()
        {
            //마우스 포인터가 UI위에 있으면
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //터렛을 선택하지 않으면
            if (buildManager.CannotBuild)
            {
                return;
            }

            rend.enabled = true;
            //rend.material.color = hoverColor;
            rend.material = hoverMaterial;

            //선택한 터렛을 건설한 비용을 가지고 있는지 잔고확인
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }
            if (turret != null || turret_upgrade != null)
            {
                rend.material.color = notenoughColor;
                return;
            }

        }*/

        /*private void OnMouseDown()
        {
            //마우스 포인터가 UI위에 있으면
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (turret != null || turret_upgrade != null)
            {
                buildManager.SelectTile(this);
                return;
            }
            if (buildManager.CannotBuild)
            {
                Debug.Log("터렛을 설치하지 못했습니다"); //터렛을 선택하지 않은 상태
                return;
            }
            BuildTurret();
        }*/
        /*public void SellTurret()
        {
            //업그레이드 터렛을 판매
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //업그레이드터렛들의 반값으로 판매
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }
            //기본 터렛을 판매
            else if (turret != null)
            {
                Destroy(turret);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                PlayerStats.AddMoney(blueprint.GetSellcost());
            }
        }*/

        /*public void UpgradeTurret()
        {
            //Debug.Log("터렛 업그레이드");
            if (blueprint == null)
            {
                //Debug.Log("업그레이드 실패했습니다");
                return;
            }
            if (PlayerStats.UseMoney(blueprint.costUpgrade))
            {
                //Effect
                GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //터렛 업그레이드 여부
                IsUpgrade = true;

                //터렛 업그레이드 생성
                turret_upgrade = Instantiate(TowerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
                Destroy(turret);
            }
        }*/

        //터렛 설치 위치
        public Vector3 GetBuildPosition()
        {
            return this.transform.position;
        }
        //마우스가 타일 안에서 밖으로 이동시
        /*private void OnMouseExit()
        {
            rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;
        }*/

        private void BuildTurret()
        {
            //설치할 터렛의 속성값 가져오기 (터렛 프리팹, 건설비용, 업그레이드 프리팹, 업그레이드 비용...)
            towerInfo = buildManager.GetTurretToBuild();

            //돈을 지불한다 100, 250
            //Debug.Log($"터렛 건설비용: {blueprint.cost}");
            //Effect
            GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            Destroy(effectGo, 2f);

            tower = Instantiate(towerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
            //Debug.Log($"건설하고 남은 돈은 {PlayerStats.Money}");
        }
    }
}