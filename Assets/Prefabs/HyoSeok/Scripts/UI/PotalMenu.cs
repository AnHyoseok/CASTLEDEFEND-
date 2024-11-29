using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

namespace MyVrSample
{
    /// <summary>
    /// 게임중 포탈 메뉴를 관리
    /// </summary>
    public class PotalMenu : MonoBehaviour
    {
        #region Variables

        //[SerializeField] private float distance = 1.5f;
        //포탈
        public GameObject potalMenu;
        //포탈 버튼
        public GameObject[] potalButton;
        public Transform[] potalsTransform;
        //플레이어 위치값
        public Transform playerTransform;

        //포탈 사운드
        public AudioClip potalSound;
        private AudioSource audioSource;

      
        #endregion

        private void Start()
        {
        
            audioSource = gameObject.AddComponent<AudioSource>();
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

        //포탈 사운드
        public void OnButtonClick()
        {
            audioSource.clip = potalSound;
            audioSource.Play();

     
        }
    }
}