using Defend.Player;
using Defend.Tower;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //타일에 설치된 타워 게임오브젝트 객체
        public GameObject towerprefab;
        private GameObject tower;
        //현재 선택된 타워 TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;

        [SerializeField] private Vector3 offset;

        //빌드매니저 객체
        private BuildManager buildManager;

        //설치 가능하면 나타나는 파티클
        public ParticleSystem particle;
        
        //설치 불가능하면 나타나는 파티클
        public ParticleSystem notparticle;

        //이펙트 프리팹
        public GameObject TowerImpectPrefab;

        //타워 업그레이드 여부
        //public bool IsUpgrade { get; private set; }

        //판매 이펙트 프리팹
        public GameObject SellImpectPrefab;
        #endregion

        private void Start()
        {
            //초기화
            particle.Stop();
            notparticle.Stop();
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            //타워가 설치되어있으면 return
            if (tower != null)
            {
                //buildManager.SelectTile(this);
                StartCoroutine(notparticlePlay());
                return;
            }
            //설치가능하면 파티클 재생
            else if(tower == null)
            {
                Debug.Log("설치할 곳");
                particle.Play();
            }
            /*if (buildManager.CannotBuild)
            {
                return;
            }

            //선택한 터렛을 건설한 비용을 가지고 있는지 잔고확인
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //타워가 설치되어있으면 return
            if (tower != null)
            {
                //buildManager.SelectTile(this);
                StartCoroutine(notparticlePlay());
                return;
            }
            //타워가 설치 가능하면 설치
            Debug.Log("설치");
            particle.Stop();
            /*if (buildManager.CannotBuild)
            {
                Debug.Log("터렛을 설치하지 못했습니다"); //터렛을 선택하지 않은 상태
                return;
            }*/
            BuildTower();
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            //Debug.Log("hover");
            particle.Stop();
            notparticle.Stop();
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            //Debug.Log("select");
            particle.Stop();
            notparticle.Stop();
        }
        //설치 되어있으면 호출되는 함수
        IEnumerator notparticlePlay()
        {
            Debug.Log("설치 되어있습니다");
            notparticle.Play();
            yield return new WaitForSeconds(2f);
            notparticle.Stop();
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
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //기본터렛들의 반값으로 판매
                //PlayerState.AddMoney();
            }
        }

        public void UpgradeTower()
        {
            Debug.Log("터렛 업그레이드");
            /*if (blueprint == null)
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
            }*/
        }

        //타워 설치 위치
        public Vector3 GetBuildPosition()
        {
            return this.transform.position + offset;
        }
        //타워 생성
        private void BuildTower()
        {
            //설치할 터렛의 속성값 가져오기 (터렛 프리팹, 건설비용, 업그레이드 프리팹, 업그레이드 비용...)
            //towerInfo = buildManager.GetTurretToBuild();

            //돈을 지불한다 100, 250
            //Debug.Log($"터렛 건설비용: {blueprint.cost}");
            //타워 생성 이펙트
            GameObject effgo = Instantiate(TowerImpectPrefab, transform.position, Quaternion.identity);
            //타일 자식으로 생성
            effgo.transform.parent = transform;
            //타워 생성
            tower = Instantiate(towerprefab, GetBuildPosition(), Quaternion.identity);
            //Debug.Log($"건설하고 남은 돈은 {PlayerStats.Money}");
            //애니메이션 시간 1.5초 후 삭제
            Destroy(effgo,1.5f);
        }
    }
}