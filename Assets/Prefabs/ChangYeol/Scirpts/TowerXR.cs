using Defend.Tower;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TowerXR : XRGrabInteractable
    {
        #region Variables
        public TowerInfo towerInfo;

        //����Ŵ��� ��ü
        private BuildManager buildManager;
        #endregion

        private void Start()
        {
            //����
            Rigidbody rigidbody = this.GetComponent<Rigidbody>();
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            buildManager.DeselectTile();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            buildManager.SelectTile(this);
        }

        /*protected override void OnActivated(ActivateEventArgs args)
        {
            base.OnActivated(args);
            buildManager.SelectTile(this);
        }*/
        void OnActionUI()
        {
            Debug.Log("act");
            buildManager.SelectTile(this);
        }
        public void SellTower()
        {
            /*//���׷��̵� �ͷ��� �Ǹ�
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //���׷��̵��ͷ����� �ݰ����� �Ǹ�
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }*/
            //�⺻ �ͷ��� �Ǹ�
            if (this != null)
            {
                Destroy(this);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                buildManager.playerState.AddMoney(1);
            }
        }

        public void UpgradeTower()
        {
            Debug.Log("�ͷ� ���׷��̵�");
            /*if (blueprint == null)
            {
                //Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (PlayerStats.UseMoney(blueprint.costUpgrade))
            {
                //Effect
                GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;

                //�ͷ� ���׷��̵� ����
                turret_upgrade = Instantiate(TowerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
                Destroy(this);
            }*/
        }
    }
}