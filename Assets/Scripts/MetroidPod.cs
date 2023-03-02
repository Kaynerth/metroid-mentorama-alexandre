using UnityEngine;

public class MetroidPod : MonoBehaviour
{
    [SerializeField] PauseMenu _completionMenu;
    [SerializeField] Character _charMove;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _bgMusic;
    [SerializeField] AudioClip _victoryMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            _charMove._isFreeToMove = false;
            _completionMenu._isPaused = true;

            _completionMenu._completionMenuText.gameObject.SetActive(true);
            _completionMenu._completionMenuPanel.gameObject.SetActive(true);
            _completionMenu._completionMenuBackground.gameObject.SetActive(true);
            _completionMenu._completionRestartButton.gameObject.SetActive(true);
            _completionMenu._completionMainMenuButton.gameObject.SetActive(true);

            _audioSource.Stop();
            _bgMusic.clip = _victoryMusic;
            _bgMusic.Play();
        }
    }
}
