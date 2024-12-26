using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Defend.Utillity;
using Defend.Enemy;

namespace Defend.Audio
{
    // AudioManager 클래스는 Unity의 오디오 믹서를 제어하는 기능을 제공
    public class AudioManager : MonoBehaviour
    {
        // AudioMixer 배열은 프로젝트에서 사용할 오디오 믹서들을 참조
        // AudioMixer는 Unity의 오디오 시스템에서 여러 오디오 트랙을 믹싱하고 처리하는 데 사
        public AudioMixer[] AudioMixers;
        [SerializeField] public Slider m_AudioMasterSlider;
        [SerializeField] public Slider m_AudioBGMSlider;
        [SerializeField] public Slider m_AudioSFXSlider;

        public AudioClip peacefulBGM;
        public AudioClip direBGM;
        private AudioClip currentBGM; // 현재 재생 중인 BGM

        private AudioSource audioSource;

        private void Awake()
        {
            m_AudioMasterSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_MASTER));
            m_AudioBGMSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_BGM));
            m_AudioSFXSlider.onValueChanged.AddListener(value => AudioUtility.SetVolume(value, Constants.AUDIO_UTIL_EFFECT));

            audioSource = GetComponent<AudioSource>();
            currentBGM = audioSource.clip;
        }

        private void Start()
        {
            //InitializeSliders();
        }

        private void Update()
        {
            // 현재 BGM 상태를 결정
            AudioClip targetBGM = ListSpawnManager.enemyAlive > 0 || ListSpawnManager.isSpawn ? direBGM : peacefulBGM;

            // BGM이 변경되었을 때만 교체
            if (currentBGM != targetBGM)
            {
                ChangeBGM(targetBGM);
            }
        }

        private void ChangeBGM(AudioClip clip)
        {
            currentBGM = clip; // 현재 BGM 업데이트
            audioSource.clip = clip;
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.Play();
        }


        /// <summary>
        /// 지정된 경로(subPath)에 해당하는 AudioMixerGroup들을 찾습니다.
        /// </summary>
        /// <param name="subPath">오디오 믹서 그룹의 경로 (예: "Master/Effect").</param>
        /// <returns>첫 번째로 발견된 AudioMixer의 매칭 그룹 배열을 반환하거나, 없으면 null을 반환.</returns>
        public AudioMixerGroup[] FindMatchingGroups(string subPath)
        {
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                AudioMixerGroup[] results = AudioMixers[i].FindMatchingGroups(subPath);
                if (results != null && results.Length != 0)
                {
                    return results;
                }
            }

            return null;
        }

        /// <summary>
        /// AudioMixer의 파라미터 값을 설정합니다.
        /// </summary>
        /// <param name="name">설정할 파라미터의 이름 (예: "Volume").</param>
        /// <param name="value">설정할 값 (float 타입).</param>
        public void SetFloat(string name, float value)
        {
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                if (AudioMixers[i] != null)
                {
                    AudioMixers[i].SetFloat(name, value);
                    InitializeSliders();
                }
            }
            
        }

        /// <summary>
        /// AudioMixer의 파라미터 값을 가져옵니다.
        /// </summary>
        /// <param name="name">가져올 파라미터의 이름.</param>
        /// <param name="value">해당 파라미터 값이 할당될 변수 (out 키워드 사용).</param>
        public void GetFloat(string name, out float value)
        {
            value = 0f;
            for (int i = 0; i < AudioMixers.Length; i++)
            {
                if (AudioMixers[i] != null)
                {
                    AudioMixers[i].GetFloat(name, out value);
                    break;
                }
            }
        }
        private void InitializeSliders()
        {
            // AudioManager에서 현재 값을 가져와 슬라이더에 반영
            m_AudioMasterSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER);
            m_AudioBGMSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM);
            m_AudioSFXSlider.value = AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT);
        }
    }
}