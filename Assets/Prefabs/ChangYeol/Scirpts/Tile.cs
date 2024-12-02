using Defend.Tower;
using UnityEngine;

namespace Defend.UI
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector]public GameObject tower;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        [HideInInspector]public TowerInfo towerInfo;

        private Vector3 offset;

        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public Transform head;
        //Ÿ�� ���׷��̵� ����
        //public bool IsUpgrade { get; private set; }
        [SerializeField] private float distance = 1.5f;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ

            buildManager = BuildManager.Instance;
        }
        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return head.position + new Vector3(head.forward.x,0,head.forward.z).normalized * distance;
        }
        //Ÿ�� ����
        public void BuildTower()
        {
            if (towerInfo == null)
            {
                buildManager.warningWindow.ShowWarning("You have not selected a build");
            }

            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo = buildManager.GetTowerToBuild();

            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Ÿ�� ����
            tower = Instantiate(towerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
            //Ÿ���� ���� �� �ִ� ������Ʈ �߰�
            tower.AddComponent<BoxCollider>();
            tower.AddComponent<TowerXR>();
            //Ÿ�� ���� ����Ʈ
            GameObject effgo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            //Ÿ�� �ڽ����� ����
            effgo.transform.parent = transform;

            Destroy(effgo, 1.5f);
        }
    }
}