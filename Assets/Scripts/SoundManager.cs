using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource _effectsAudioSource;
    [SerializeField] Slider _effectsSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] AudioSource _musicAudioSource;
    [SerializeField] AudioSource _assistantEffectsAudioSource;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreenScene" || SceneManager.GetActiveScene().name == "IntroScene")
        {
            _musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolumeSave", 0.5f);
            _effectsAudioSource.volume = PlayerPrefs.GetFloat("EffectsVolumeSave", 0.5f);
            if (_assistantEffectsAudioSource != null)
            {
                _assistantEffectsAudioSource.volume = PlayerPrefs.GetFloat("EffectsVolumeSave", 0.5f);
            }
        }

        else
        {
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeSave", 0.5f);
            _effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolumeSave", 0.5f);
            _effectsAudioSource.volume = _effectsSlider.value;
            _musicAudioSource.volume = _musicSlider.value;
            if (_assistantEffectsAudioSource != null)
            {
                _assistantEffectsAudioSource.volume = _effectsSlider.value;
            }
        }
    }

    public void EffectsSlider()
    {
        _effectsAudioSource.volume = _effectsSlider.value;
        _assistantEffectsAudioSource.volume = _effectsSlider.value;
    }

    public void MusicSlider()
    {
        _musicAudioSource.volume = _musicSlider.value;
    }
}
