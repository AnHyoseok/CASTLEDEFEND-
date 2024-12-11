
using Defend.XR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
        public Image targetImage;
        public Sprite handSprite; 
        public Sprite axeSprite;
        public Sprite pickaxeSprite;

        [SerializeField] private GameObject axe;
        [SerializeField] private GameObject pickAxe;
        public InputActionProperty property;
        #endregion

        private void Start()
        {
            targetImage.sprite = axeSprite;
            UpdateTool();
        }

        private void Update()
        {
            //if (InputManager.Instance == null)
            //{
            //    return;
            //}
            //   x키입력시 무기교체,
            if (property.action.WasCompletedThisFrame() || Input.GetKeyDown(KeyCode.Z))
            {
                
                //Debug.Log("wepon swap");
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
                    targetImage.sprite = pickaxeSprite;
                    return;
                case ToolType.Axe:
                    currentTool = ToolType.PickAxe;
                    targetImage.sprite = handSprite;
                    return;
                case ToolType.PickAxe:
                    currentTool = ToolType.None;
                    targetImage.sprite = axeSprite;
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