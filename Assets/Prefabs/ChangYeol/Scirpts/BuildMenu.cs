using Defend.Tower;
using Defend.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildMenu : MonoBehaviour
{
    #region Variables
    private BuildManager buildManager;
    //기본 타워 정보값
    public TowerInfo cannonTower;
    //빌드 UI 활성화 /비활성화
    public GameObject buildUI;

    //public InputActionProperty showButton;

    //private bool IsButton;
    public Tile tile;
    #endregion

    private void Start()
    {
        //초기화
        buildManager = BuildManager.Instance;
    }


    //기본 터렛 버튼을 클릭시 호출
    public void SelectCannonTower()
    {
        //Debug.Log("기본 터렛을 선택 하였습니다");
        //설치할 터렛에 기본 터렛(프리팹)을 저장
        buildManager.SetTowerToBuild(cannonTower);
        Debug.Log("디버그");
        //tile.BuildTower();
    }
}
