using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.UI;
namespace Defend.UI
{
    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    /*
    public class ButtonClick : MonoBehaviour
    {
        #region Variables
        public AudioClip clickSound;
        private AudioSource audioSource;
        public Button[] buttons;
        #endregion


        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clickSound; 
            audioSource.playOnAwake = false; // 자동 재생 방지

         
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        public void OnButtonClick()
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }
    */

    public class ButtonClick : MonoBehaviour
    {
        public AudioClip clickSound;    //클릭 사운드
        public AudioClip potalSound;    //포탈 사운드
        public AudioMixerGroup audioMixerGroup;
        [SerializeField] private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.outputAudioMixerGroup = audioMixerGroup;
            }
            else
            {
                Debug.LogWarning("Audio Source not found");
            }
            AddButtons();  // 모든 버튼에 AudioSource를 추가
        }

        // 모든 버튼에 AudioSource를 추가하고 클릭 이벤트를 연결하는 메서드
        void AddButtons()
        {
            // 씬 내 모든 버튼을 찾습니다.
            Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            //찾아온 모든 버튼 중 "Teleport"버튼들은 텔레포트 사운드, 다른 모든 버튼들은 클릭 사운드 AddListener로 이벤트 할당
            foreach (Button button in buttons)
            {
                if (button.name.Contains("Teleport"))
                {
                    button.onClick.AddListener(() => PlayClickSound(potalSound));  // 클릭 이벤트에 사운드 재생 추가
                }
                else
                {
                    button.onClick.AddListener(() => PlayClickSound(clickSound));  // 클릭 이벤트에 사운드 재생 추가
                }

            }
        }

        // 클릭 시 클릭 사운드를 재생하는 메서드
        void PlayClickSound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}