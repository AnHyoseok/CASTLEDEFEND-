using Defend.XR;
using UnityEngine;

namespace Defend.UI
{
    public class PlayerUI : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject gameMenuCanvas;
        [SerializeField] private GameObject buildCanvas;
        [SerializeField] private GameObject upgradeCanvas;
        [SerializeField] private GameObject towerCanvas;
        [SerializeField] private GameObject skilCanvas;
        [SerializeField] private Animator[] animators;
        private bool isOpen;
        #endregion

        private void Update()
        {
            //Y버튼 누를시 메뉴 오픈
            if (InputManager.Instance.GetRightSecondaryButton() || Input.GetKeyDown(KeyCode.X))
            {
                if (!isOpen)
                {
                    // 메뉴가 열리지 않은 경우, gameMenuCanvas 열기
                    isOpen = true;
                    gameMenuCanvas.SetActive(true);
                }
                else
                {
                  
                    CloseAllCanvases();
                }
            }
        }
        // 메뉴가 열려 있는 경우 모든 캔버스 닫기
        public void CloseAllCanvases()
        {
            isOpen = false;
            foreach (var animator in animators)
            { // 애니메이션 초기화
                animator.Rebind();
                animator.Update(0); 
            }

            gameMenuCanvas.SetActive(false);
            buildCanvas.SetActive(false);
            upgradeCanvas.SetActive(false);
            towerCanvas.SetActive(false);
            skilCanvas.SetActive(false);
        }

        public void Build()
        {
            //Debug.Log("buildopen");
            gameMenuCanvas.SetActive(false);
            buildCanvas.SetActive(true);
        }

        public void Upgrade()
        {
            //Debug.Log("Upgradeopen");
            gameMenuCanvas.SetActive(false);
            upgradeCanvas.SetActive(true );
        }

        public void Tower()
        {
            //Debug.Log("Shopopen");
            gameMenuCanvas.SetActive(false);
            towerCanvas.SetActive(true);
        }

        public void Skill()
        {
            //Debug.Log("Skillopen");
            gameMenuCanvas.SetActive(false);
            skilCanvas.SetActive(true);
        }
    }
}