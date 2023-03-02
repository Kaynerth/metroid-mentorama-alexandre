using UnityEngine;

public class PickUpHealing : MonoBehaviour
{
    [SerializeField] GameObject _samus;

    [SerializeField] int _healingOrb;

    GameObject _soundManager;

    [SerializeField] AudioClip _efAudio;

    void Start()
    {
        _soundManager = GameObject.Find("SoundManager");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _samus = GameObject.Find("Samus");
            _samus.gameObject.GetComponent<Character>().Healing(_healingOrb);
            _soundManager.GetComponent<AudioSource>().PlayOneShot(_efAudio);
            Destroy(gameObject);
        }
    }
}
