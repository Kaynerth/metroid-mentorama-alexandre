using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public bool _isPaused = false;
    [HideInInspector] public bool _isCompleted = false;

    [Header("Pause Menu")]
    [SerializeField] TMP_Text _pauseMenuText;
    [SerializeField] Image _pauseMenuPanel;
    [SerializeField] Image _pauseMenuBackground;
    [SerializeField] TMP_Text _pauseMenuMusic;
    [SerializeField] TMP_Text _pauseMenuEffects;
    [SerializeField] Button _pauseMenuBack;
    [SerializeField] Slider _pauseMusicSlider;
    [SerializeField] Slider _pauseEffectsSlider;

    [Header("Character Control")]
    [SerializeField] Character _charMove;

    [Header("Completion Menu")]
    public TMP_Text _completionMenuText;
    public Image _completionMenuPanel;
    public Image _completionMenuBackground;
    public Button _completionRestartButton;
    public Button _completionMainMenuButton;

    private void Start()
    {
        Time.timeScale = 1;

        _pauseMenuText.gameObject.SetActive(false);
        _pauseMenuPanel.gameObject.SetActive(false);
        _pauseMenuBackground.gameObject.SetActive(false);
        _pauseMenuMusic.gameObject.SetActive(false);
        _pauseMenuEffects.gameObject.SetActive(false);
        _pauseMenuBack.gameObject.SetActive(false);
        _pauseMusicSlider.gameObject.SetActive(false);
        _pauseEffectsSlider.gameObject.SetActive(false);

        _completionMenuText.gameObject.SetActive(false);
        _completionMenuPanel.gameObject.SetActive(false);
        _completionMenuBackground.gameObject.SetActive(false);
        _completionRestartButton.gameObject.SetActive(false);
        _completionMainMenuButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _isPaused == false && _charMove._isFreeToMove && _isCompleted == false)
        {
            Time.timeScale = 0;
            _charMove._isFreeToMove = false;

            _pauseMenuText.gameObject.SetActive(true);
            _pauseMenuPanel.gameObject.SetActive(true);
            _pauseMenuBackground.gameObject.SetActive(true);
            _pauseMenuMusic.gameObject.SetActive(true);
            _pauseMenuEffects.gameObject.SetActive(true);
            _pauseMenuBack.gameObject.SetActive(true);
            _pauseMusicSlider.gameObject.SetActive(true);
            _pauseEffectsSlider.gameObject.SetActive(true);

            _isPaused = true;
        }

        else if (Input.GetKeyDown(KeyCode.Tab) && _isPaused && _isCompleted == false)
        {
            Time.timeScale = 1;
            _charMove._isFreeToMove = true;

            _isPaused = false;
            _pauseMenuText.gameObject.SetActive(false);
            _pauseMenuPanel.gameObject.SetActive(false);
            _pauseMenuBackground.gameObject.SetActive(false);
            _pauseMenuMusic.gameObject.SetActive(false);
            _pauseMenuEffects.gameObject.SetActive(false);
            _pauseMenuBack.gameObject.SetActive(false);
            _pauseMusicSlider.gameObject.SetActive(false);
            _pauseEffectsSlider.gameObject.SetActive(false);

            PlayerPrefs.SetFloat("MusicVolumeSave", _pauseMusicSlider.value);
            PlayerPrefs.SetFloat("EffectsVolumeSave", _pauseEffectsSlider.value);
        }
    }

    public void OnClickBackButton()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            _charMove._isFreeToMove = true;

            _isPaused = false;
            _pauseMenuText.gameObject.SetActive(false);
            _pauseMenuPanel.gameObject.SetActive(false);
            _pauseMenuBackground.gameObject.SetActive(false);
            _pauseMenuMusic.gameObject.SetActive(false);
            _pauseMenuEffects.gameObject.SetActive(false);
            _pauseMenuBack.gameObject.SetActive(false);
            _pauseMusicSlider.gameObject.SetActive(false);
            _pauseEffectsSlider.gameObject.SetActive(false);

            PlayerPrefs.SetFloat("MusicVolumeSave", _pauseMusicSlider.value);
            PlayerPrefs.SetFloat("EffectsVolumeSave", _pauseEffectsSlider.value);
        }
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("TitleScreenScene");
    }
}
