using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] int _damage;

    [SerializeField] Transform _spike1Direction;
    [SerializeField] Transform _spike2Direction;
    [SerializeField] Transform _spike3Direction;
    [SerializeField] Transform _spike4Direction;
    [SerializeField] Transform _spike5Direction;
    [SerializeField] Transform _deathPosition;

    [SerializeField] GameObject _enemyDeathEffect;
    [SerializeField] GameObject _healingOrb;
    [SerializeField] GameObject _bigHealingOrb;
    [SerializeField] GameObject _ammo;
    [SerializeField] GameObject _spikePrefab;

    [SerializeField] AudioClip _enemyDeathSound;

    GameObject _audioManager;
    AudioSource _audioSource;


    GameObject _samus;
    Character _player;

    void Start()
    {
        _samus = GameObject.Find("Samus");
        _player = _samus.GetComponent<Character>();

        _audioManager = GameObject.Find("SoundManager");
        _audioSource = _audioManager.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Death();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.TakeDamage(_damage);
            if (_player._isFacingRight)
            {
                _player._rigidBody.AddForce(new Vector2(-2f, 5f), ForceMode2D.Impulse);
            }
            else if (_player._isFacingLeft)
            {
                _player._rigidBody.AddForce(new Vector2(2f, 5f), ForceMode2D.Impulse);
            }
        }
    }

    void Spike()
    {
        Instantiate(_spikePrefab, _spike1Direction.position, _spike1Direction.rotation);
        Instantiate(_spikePrefab, _spike2Direction.position, _spike2Direction.rotation);
        Instantiate(_spikePrefab, _spike3Direction.position, _spike3Direction.rotation);
        Instantiate(_spikePrefab, _spike4Direction.position, _spike4Direction.rotation);
        Instantiate(_spikePrefab, _spike5Direction.position, _spike5Direction.rotation);
    }

    void Death()
    {
        Instantiate(_enemyDeathEffect, _deathPosition.position, _deathPosition.rotation);
        _audioSource.PlayOneShot(_enemyDeathSound);
        Destroy(gameObject);

        if (_samus.GetComponent<Character>()._health < 50)
        {
            Instantiate(_bigHealingOrb, _deathPosition.position, _deathPosition.rotation);
        }

        else if (_samus.GetComponent<Character>()._health < 100)
        {
            Instantiate(_healingOrb, _deathPosition.position, _deathPosition.rotation);
        }

        else if (_samus.GetComponent<Character>()._ammo < 20)
        {
            Instantiate(_ammo, _deathPosition.position, _deathPosition.rotation);
        }
    }
}
