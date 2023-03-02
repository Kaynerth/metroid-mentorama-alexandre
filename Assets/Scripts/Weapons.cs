using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    [SerializeField] Transform _firePosition;

    [SerializeField] GameObject _bullet1Prefab;
    [SerializeField] GameObject _bullet2Prefab;
    [SerializeField] GameObject _samus;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipShoot;
    [SerializeField] AudioClip _audioClipMissile;
    [SerializeField] AudioClip _audioEmptyMissile;

    [SerializeField] Image _missileIcon;
    [SerializeField] Sprite _missileActiveSprite;
    [SerializeField] Sprite _missileDeactiveSprite;

    [SerializeField] bool _isMissileActive = false;

    float missileCooldown;

    Character _charMovement;

    void Start()
    {
        _samus = GameObject.Find("Samus");
        _charMovement = _samus.gameObject.GetComponent<Character>();
    }

    void Update()
    {
        missileCooldown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && !_isMissileActive && _charMovement._isFreeToMove)
        {
            _isMissileActive = true;
            _missileIcon.sprite = _missileActiveSprite;
        }

        else if (Input.GetKeyDown(KeyCode.X) && _isMissileActive && _charMovement._isFreeToMove)
        {
            _isMissileActive = false;
            _missileIcon.sprite = _missileDeactiveSprite;
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)) && _charMovement._isFreeToMove && !_isMissileActive)
        {
            Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)) && _charMovement._isFreeToMove && _isMissileActive)
        {
            if (_charMovement._ammo > 0)
            {
                ShootMissile();
            }
            else
            {
                _audioSource.PlayOneShot(_audioEmptyMissile);
            }
        }
    }

    void Shoot()
    {
        Instantiate(_bullet1Prefab, _firePosition.position, _firePosition.rotation);
        _audioSource.PlayOneShot(_audioClipShoot);
    }

    void ShootMissile()
    {
        if (missileCooldown >= 0.7f && _charMovement._ammo > 0)
        {
            missileCooldown = 0;

            Instantiate(_bullet2Prefab, _firePosition.position, _firePosition.rotation);
            _audioSource.PlayOneShot(_audioClipMissile);
            _charMovement._ammo -= 1;
        }
    }
}
