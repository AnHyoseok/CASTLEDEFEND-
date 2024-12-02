using Defend.Tower;
using System.Collections;
using UnityEngine;

namespace Defend.UI
{
    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;
        //�⺻ Ÿ�� ������
        public TowerInfo cannonTower;

        public Tile tile;

        public GameObject BuildUI;

        private BoxCollider Trirggerbox;

        private bool isSelect;
        public bool IsSelect
        {
            get
            {
                return isSelect;
            }
            set
            {
                isSelect = value;
            }
        }
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            //�÷��̾�� box�ݶ��̴� �߰��ϰ� trigger�� �ֺ��� �ִ� �ݶ��̴��� ������ ���â�� ���
            Trirggerbox = buildManager.playerState.gameObject.AddComponent<BoxCollider>();
            Trirggerbox.isTrigger = true;
            Trirggerbox.center = new Vector3(0, 0.3f, 0);
            Trirggerbox.size = new Vector3(2.5f, 0.3f, 2.5f);
            IsSelect = false;
        }


        //�⺻ �ͷ� ��ư�� Ŭ���� ȣ��
        public void SelectCannonTower()
        {
            if (IsSelect)
            {
                StartCoroutine(TriggerWarning(cannonTower));
                return;
            }
            else if (!buildManager.playerState.SpendMoney(cannonTower.cost2))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }

            if (buildManager.playerState.SpendMoney(cannonTower.cost2))
            {
                //Debug.Log("�⺻ �ͷ��� ���� �Ͽ����ϴ�");
                //��ġ�� �ͷ��� �⺻ �ͷ�(������)�� ����
                buildManager.SetTowerToBuild(cannonTower);
                tile.BuildTower();
                BoxCollider boxCollider = tile.tower.GetComponent<BoxCollider>();
                boxCollider.size = new Vector3(1, 2.516554f, 1);
                boxCollider.center = new Vector3(0, 0.2562535f, 0);
            }
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
        }
        IEnumerator TriggerWarning(TowerInfo tower)
        {
            buildManager.warningWindow.ShowWarning($"There's an {tower.upgradeTower.name} in front of me");
            yield return new WaitForSeconds(2);
            buildManager.warningWindow.ShowWarning($"Create it somewhere else");
            yield return new WaitForSeconds(5);
        }
    }
}