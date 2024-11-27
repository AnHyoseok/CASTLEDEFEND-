using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

namespace MyVrSample
{
    /// <summary>
    /// 게임중 메뉴 UI 관리하는 클래스
    /// </summary>
    public class GameMenuManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float distance = 1.5f;
        //포탈
        public GameObject potalMenu;
        //포탈 버튼
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //플레이어 위치값
        public Transform playerTransform;
        #endregion

        //앞에띄우기
        void Update()
        {
        
            //show 설정
            if(potalMenu.activeSelf)
            {
                potalMenu.transform.position = playerTransform.position + new Vector3(playerTransform.forward.x, 0f, playerTransform.forward.z).normalized * distance;
                potalMenu.transform.LookAt(new Vector3(playerTransform.position.x, potalMenu.transform.position.y, playerTransform.position.z));
                potalMenu.transform.forward *= -1;
            }
        }
        //포탈 UI 띄우기
        public void OnPotalUI()
        {
            potalMenu.SetActive(true);
           
        }

        //포탈 이동 버튼
        public void Teleportation(int index)
        {
            if (index >= 0 && index < potalsTransform.Length)
            {
                playerTransform.position = potalsTransform[index].position;
            }
        }

        //UI 닫기
        public void OffPotalUI()
        {
            potalMenu.SetActive(false); 
        }
    }
}