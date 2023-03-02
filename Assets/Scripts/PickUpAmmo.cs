using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    [SerializeField] GameObject _samus;

    [SerializeField] int _ammo;

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
            _samus.gameObject.GetComponent<Character>().Ammo(_ammo);
            _soundManager.GetComponent<AudioSource>().PlayOneShot(_efAudio);
            Destroy(gameObject);
        }
    }
}
