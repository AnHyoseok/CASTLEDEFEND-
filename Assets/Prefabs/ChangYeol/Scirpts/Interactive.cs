using MyFps;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Defend.UI
{
    public abstract class Interactive : MonoBehaviour
    {
        #region Variables
        public GameObject gameMenu;
        //public InputActionProperty showButton;

        private Transform head;
        [SerializeField] private float distance = 1.5f;
        #endregion
        void Start()
        {
            head = Camera.main.transform;
        }

        private void Update()
        {
            distance = PlayerCasting.distanceFromTarget;

            if (distance <= 2)
            {
                Toggle();
            }
        }
        public void Toggle()
        {
            gameMenu.SetActive(true);

            distance = (distance < 1.5f) ? distance - 0.1f : 1.5f;

            gameMenu.transform.position = head.position + new Vector3(head.forward.x, 0f, head.forward.z).normalized * distance;
            gameMenu.transform.LookAt(new Vector3(head.position.x, gameMenu.transform.position.y, head.position.z));
            gameMenu.transform.forward *= -1;
        }
    }
}