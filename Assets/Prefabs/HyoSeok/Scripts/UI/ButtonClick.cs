using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace Defend.UI
{
    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    public class ButtonClick : MonoBehaviour
    {
        #region Variables
        public AudioClip clickSound;
        private AudioSource audioSource;
        public Button[] buttons;
        #endregion


        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clickSound; 
            audioSource.playOnAwake = false; // 자동 재생 방지

         
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        public void OnButtonClick()
        {
            audioSource.Play();
        }
    }
}