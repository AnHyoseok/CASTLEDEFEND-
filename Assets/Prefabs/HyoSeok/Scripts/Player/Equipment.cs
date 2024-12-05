
using Defend.XR;
using UnityEngine;

namespace Defend.Player
{
    /// <summary>
    /// 무기 시스템 , 교체
    /// </summary>
    public class Equipment : MonoBehaviour
    {
        #region Variables
        private enum ToolType { None, Axe, PickAxe }
        private ToolType currentTool = ToolType.None;

        [SerializeField] private GameObject axe;
        [SerializeField] private GameObject pickAxe;
        #endregion

        private void Start()
        {
            UpdateTool();
        }

        private void Update()
        {
            //if (InputManager.Instance == null)
            //{
            //    return;
            //}
            //   x키입력시 무기교체,
            if (InputManager.Instance.GetLeftSecondaryButton() || Input.GetKeyDown(KeyCode.Z))
            {
                
                Debug.Log("wepon swap");
                Swap();
                UpdateTool();
            }
        }

        //다음무기로 전환
        void Swap()
        {
            switch (currentTool)
            {
                case ToolType.None:
                    currentTool = ToolType.Axe;
                    return;
                case ToolType.Axe:
                    currentTool = ToolType.PickAxe;
                    return;
                case ToolType.PickAxe:
                    currentTool = ToolType.None;
                    return;
            }

        }
        //도구 업데이트
        void UpdateTool()
        {
            axe.SetActive(currentTool == ToolType.Axe);
            pickAxe.SetActive(currentTool == ToolType.PickAxe);
        }

    }
}