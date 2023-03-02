using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    enum AnimSamus
    {
        Idle,
        Running,
        Jumping,
        Crouching,
        TakingDamage,
        Death
    }

    public Rigidbody2D _rigidBody;

    AnimSamus _animation;

    Vector2 _speed;

    Animator _samusAnim;

    [SerializeField] Transform _fireDirection;

    [SerializeField] AudioSource _bgAudio;
    [SerializeField] AudioClip _bgMusic;

    [SerializeField] AudioSource _efAudio;
    [SerializeField] AudioSource _efAssistantAudio;
    [SerializeField] AudioClip _efRunningSound;
    [SerializeField] AudioClip _efJumpingSound;
    [SerializeField] AudioClip _efLandingSound;
    [SerializeField] AudioClip _efTakingDamage;
    [SerializeField] AudioClip _efDeathSound;

    [SerializeField] TMP_Text _healthValue;
    [SerializeField] TMP_Text _ammoValue;

    [SerializeField] Transform _gameCamera;
    float _cameraDistanceZ = -10f;

    float _jumpForce;
    float _moveHorizontal;
    float _moveVertical;
    float _jump;

    public int _health;
    public int _ammo;

    bool _isJumping;
    bool _isRunning;
    bool _isCrouching;
    bool _isTakingDamage;
    bool _isDead;

    public bool _isFacingRight;
    public bool _isFacingLeft;

    [HideInInspector] public bool _isFreeToMove;

    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _samusAnim = gameObject.GetComponent<Animator>();

        _animation = AnimSamus.Idle;
        _isFacingLeft = true;

        _speed = new Vector2(2f, 0f);
        _jumpForce = 10f;
    }

    void Update()
    {
        _healthValue.text = _health.ToString();
        _ammoValue.text = _ammo.ToString();

        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
        _jump = Input.GetAxisRaw("Jump");

        if (!_isRunning)
        {
            _animation = AnimSamus.Idle;
        }

        if (_isRunning && !_isCrouching)
        {
            _animation = AnimSamus.Running;
        }

        if (_isJumping)
        {
            _animation = AnimSamus.Jumping;
        }

        if (_isCrouching)
        {
            _animation = AnimSamus.Crouching;
        }

        if (_isTakingDamage)
        {
            _animation = AnimSamus.TakingDamage;
        }

        if (_isDead)
        {
            _animation = AnimSamus.Death;
        }

        switch (_animation)
        {
            case AnimSamus.Idle:
                _samusAnim.SetTrigger("Idle");
                break;

            case AnimSamus.Running:
                _samusAnim.SetTrigger("Running");
                break;

            case AnimSamus.Jumping:
                _samusAnim.SetTrigger("Jumping");
                break;

            case AnimSamus.Crouching:
                _samusAnim.SetTrigger("Crouching");
                break;

            case AnimSamus.TakingDamage:
                _samusAnim.SetTrigger("TakingDamage");
                break;

            case AnimSamus.Death:
                _samusAnim.SetTrigger("Death");
                break;
        }

        _gameCamera.transform.position = new Vector3(_rigidBody.transform.position.x, _rigidBody.transform.position.y, _rigidBody.transform.position.z + _cameraDistanceZ);
    }

    void FixedUpdate()
    {
        if (_moveHorizontal > 0.1f || _moveHorizontal < -0.1f && _isFreeToMove)
        {
            _isRunning = true;
            if (!_isCrouching && _isFreeToMove)
            {
                _rigidBody.position += _moveHorizontal * _speed * Time.fixedDeltaTime;
            }
        }
        else
        {
            _isRunning = false;
        }

        if (_moveHorizontal > 0.1f && _isFreeToMove)
        {
            if (_isFacingLeft)
            {
                _isFacingRight = true;
                _isFacingLeft = false;
                _rigidBody.transform.Rotate(0f, 180f, 0f);
            }
        }
        if (_moveHorizontal < -0.1f && _isFreeToMove)
        {
            if (_isFacingRight)
            {
                _isFacingRight = false;
                _isFacingLeft = true;
                _rigidBody.transform.Rotate(0f, -180f, 0f);
            }
        }

        if (_moveVertical < -0.1f && _isFreeToMove && !_isJumping && !_isRunning)
        {
            _isCrouching = true;
            _fireDirection.transform.localPosition = new Vector3(-0.144f, 0.152f, 0f);
        }
        else if (_moveVertical > 0.1f && _isFreeToMove && !_isJumping && !_isRunning)
        {
            _isCrouching = false;
            _fireDirection.transform.localPosition = new Vector3(-0.172f, 0.26f, 0f);
        }

        if (_jump > 0.1f && !_isJumping && _isFreeToMove && !_isCrouching)
        {
            _rigidBody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floors")
        {
            _isJumping = false;
            _isTakingDamage = false;
            _efAudio.Stop();
            OnLanding();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floors")
        {
            _isJumping = true;
            OnJumping();
        }
    }

    void OnStartAnimationComplete()
    {
        _bgAudio.clip = _bgMusic;
        _bgAudio.Play();
        _isFreeToMove = true;
    }

    void OnJumping()
    {
        _efAudio.clip = _efJumpingSound;
        _efAudio.Play();
    }

    void OnRunning()
    {
        _efAudio.PlayOneShot(_efRunningSound);
    }

    void OnLanding()
    {
        _efAudio.PlayOneShot(_efLandingSound);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _efAudio.PlayOneShot(_efTakingDamage);
        _isTakingDamage = true;

        if (_health <= 0)
        {
            _health = 0;
            _isDead = true;
            _isFreeToMove = false;
            _efAudio.PlayOneShot(_efDeathSound);
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }

        if (_health <= 50)
        {
            _efAssistantAudio.Play();
        }
    }

    public void Healing(int heal)
    {
        _health += heal;

        if (_health > 100)
        {
            _health = 100;
        }

        if (_health > 50)
        {
            _efAssistantAudio.Stop();
        }
    }

    public void Ammo(int ammo)
    {
        _ammo += ammo;

        if (_ammo > 20)
        {
            _ammo = 20;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("InGameScene");
    }
}
