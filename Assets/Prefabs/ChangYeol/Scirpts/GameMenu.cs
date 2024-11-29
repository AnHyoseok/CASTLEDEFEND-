using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public class GameMenu : MonoBehaviour
    {
        #region Variables
        public GameObject gameMenu;
        public InputActionProperty showButton;

        public Transform head;
        [SerializeField] private float distance = 1.5f;
        #endregion
        private void Update()
        {
            if (showButton.action.WasPressedThisFrame())
            {
                Toggle();
            }
        }
        public void Toggle()
        {
            gameMenu.SetActive(!gameMenu.activeSelf);

            if (gameMenu.activeSelf)
            {
                gameMenu.transform.position = head.position + new Vector3(head.forward.x + -0.5f, 0f, head.forward.z).normalized * distance;
                gameMenu.transform.LookAt(new Vector3(head.position.x, gameMenu.transform.position.y, head.position.z));
                gameMenu.transform.forward *= -1;
            }
        }
    }
}