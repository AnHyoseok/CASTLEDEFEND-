using UnityEngine;

namespace Defend.UI
{
    public class MiniMapCamera : MonoBehaviour
    {
        #region Variables
        //player 위치
        public Transform player;
        //미니맵 UI
        public GameObject miniMapUI;
        //현재 위치
        private Transform currentPosition;
        //

        //미니맵 카메라
        private Camera miniMapcamera;
        #endregion
        private void Start()
        {
            //참조
            miniMapcamera = GetComponent<Camera>();
            //현재 player 위치 저장
            //currentPosition = Camera.main.transform;
        }

        private void LateUpdate()
        {
            //미니맵에서 보여지는 미니맵 카메라가 player 위치를 따라다닌다
            miniMapcamera.transform.position = new Vector3(player.transform.position.x, miniMapcamera.transform.position.y, player.transform.position.z);
            //현재 player 위치 저장
            currentPosition = Camera.main.transform;
        }
        //움직일 때는 UI가 비활성화 움직이지 않을 때는 활성화
        void PlayerMoveUI()
        {
            //player가 움직이고 있는지 아닌지
            if (player.position == currentPosition.position)
            {
                miniMapUI.SetActive(false);
            }
            else
            {
                miniMapUI.SetActive(true);
            }
        }
    }
}