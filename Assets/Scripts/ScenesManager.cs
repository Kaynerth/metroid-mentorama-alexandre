using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Animator _fadeAnimator;
    public Animator _introAnimator;

    [SerializeField] AudioSource _effectAudioSource;
    [SerializeField] AudioClip _effectAudioClip;

    public bool _isSkippable;
    bool _skipIntro;
    int _skipCount;

    void Start()
    {
        _skipCount = 0;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && SceneManager.GetActiveScene().name == "TitleScreenScene")
        {
            FadeOutScene();
            _effectAudioSource.PlayOneShot(_effectAudioClip);
        }

        if (_isSkippable)
        {
            _skipIntro = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "IntroScene" && _skipIntro)
        {
            _introAnimator.SetTrigger("NextIntro");
            _skipIntro = false;
            _skipCount += 1;

            if (_skipCount >= 3)
            {
                FadeOutScene();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void FadeOutScene()
    {
        _fadeAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreenScene")
        {
            SceneManager.LoadScene("IntroScene");
        }

        else if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            SceneManager.LoadScene("InGameScene");
        }
    }
}
