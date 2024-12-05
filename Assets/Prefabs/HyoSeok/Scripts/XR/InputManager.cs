using UnityEngine;
using UnityEngine.XR;
namespace Defend.XR
{
    public class InputManager : MonoBehaviour
    {
        #region Variables
        public static InputManager Instance { get; private set; }

        private InputDevice leftHandDevice;
        private InputDevice rightHandDevice;
        #endregion

        private void Awake()
        {
            // 싱글톤 인스턴스 설정
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // 입력 장치 초기화
            leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        // 왼쪽 손의 기본 버튼 (A 버튼)
        public bool GetLeftPrimaryButton()
        {
            //Debug.Log("222222");
            leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool value);
            return value;
        }

        // 오른쪽 손의 기본 버튼 (B 버튼)
        public bool GetRightPrimaryButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool value);
            return value;
        }

        // 왼쪽 손의 보조 버튼 (X 버튼)
        public bool GetLeftSecondaryButton()
        {
            //Debug.Log("33333");
            leftHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool value);
            return value;
        }

        // 오른쪽 손의 보조 버튼 (Y 버튼)
        public bool GetRightSecondaryButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool value);
            return value;
        }

        // 왼쪽 손의 그립 버튼 (그립을 잡을 때)
        public bool GetLeftGripButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool value);
            return value;
        }

        // 오른쪽 손의 그립 버튼 (그립을 잡을 때)
        public bool GetRightGripButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool value);
            return value;
        }

        // 왼쪽 손의 트리거 버튼 (트리거를 누를 때)
        public bool GetLeftTriggerButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value);
            return value;
        }

        // 오른쪽 손의 트리거 버튼 (트리거를 누를 때)
        public bool GetRightTriggerButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool value);
            return value;
        }

        // 왼쪽 손의 메뉴 버튼 (메뉴 열기)
        public bool GetLeftMenuButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool value);
            return value;
        }

        // 오른쪽 손의 메뉴 버튼 (메뉴 열기)
        public bool GetRightMenuButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool value);
            return value;
        }

        // 왼쪽 손의 터치패드 클릭 버튼
        public bool GetLeftTouchpadButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value);
            return value;
        }

        // 오른쪽 손의 터치패드 클릭 버튼
        public bool GetRightTouchpadButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool value);
            return value;
        }

        // 왼쪽 손의 터치패드 아날로그 입력
        public Vector2 GetLeftTouchpadAxis()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value);
            return value;
        }

        // 오른쪽 손의 터치패드 아날로그 입력
        public Vector2 GetRightTouchpadAxis()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 value);
            return value;
        }

        // 왼쪽 손의 터치패드 터치 여부
        public bool GetLeftTouchButton()
        {
            leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value);
            return value;
        }

        // 오른쪽 손의 터치패드 터치 여부
        public bool GetRightTouchButton()
        {
            rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool value);
            return value;
        }
    }
}