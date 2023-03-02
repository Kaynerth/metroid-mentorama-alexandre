using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] GameObject _impactEffect;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    GameObject _soundManager;

    void Start()
    {
        _soundManager = GameObject.Find("SoundManager");
        _audioSource = _soundManager.GetComponent<AudioSource>();
    }

    void Update()
    {
        _rigidbody.velocity = transform.right * _speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Floors")
        {
            Instantiate(_impactEffect, transform.position, transform.rotation);
            _audioSource.PlayOneShot(_audioClip);

            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}
