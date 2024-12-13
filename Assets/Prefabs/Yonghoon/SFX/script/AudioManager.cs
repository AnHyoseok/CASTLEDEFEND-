using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Defend.Utillity
{
    // AudioManager 클래스는 Unity의 오디오 믹서를 제어하는 기능을 제공
    public class AudioManager : MonoBehaviour
    {
        // AudioMixer 배열은 프로젝트에서 사용할 오디오 믹서들을 참조
        // AudioMixer는 Unity의 오디오 시스템에서 여러 오디오 트랙을 믹싱하고 처리하는 데 사
        public AudioMixer[] AudioMixers;
        [SerializeField] private Slider m_AudioMasterSlider;
        [SerializeField] private Slider m_AudioBGMSlider;
        [SerializeField] private Slider m_AudioSFXSlider;

        private void Awake()
        {
            m_AudioMasterSlider.onValueChanged.AddListener(AudioUtility.SetMasterVolume);
            m_AudioBGMSlider.onValueChanged.AddListener(AudioUtility.SetBGMVolume);
            m_AudioSFXSlider.onValueChanged.AddListener(AudioUtility.SetSFXVolume);
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

    }
}