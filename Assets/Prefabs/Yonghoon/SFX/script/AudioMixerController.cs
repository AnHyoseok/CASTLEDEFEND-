using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using Defend.Utillity;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicSFXSlider;

    private void Awake()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(AudioUtility.SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(AudioUtility.SetBGMVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(AudioUtility.SetSFXVolume);
    }

    /*
    Mathf.Log10을 사용하는 이유는 슬라이더의 선형 값을 로그 스케일로 변환하여 데시벨 단위의 볼륨 설정에 맞추고, 사용자에게 더 자연스러운 볼륨 변화를 제공
    Mathf.Log10(flaot value)함수는 value가 0일때 -Infinity를 반환하므로 슬라이더 값에서 Min Value를 0.001로 설정후 사용 또는 코드에 처리해줘야함
    */

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("MASTER", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetEffectVolume(float volume)
    {
        m_AudioMixer.SetFloat("EFFECT", Mathf.Log10(volume) * 20);
    }
}