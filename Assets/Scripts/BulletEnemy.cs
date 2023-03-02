using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] int _damage;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] GameObject _impactEffect;

    void Update()
    {
        _rigidbody.velocity = transform.right * _speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_impactEffect, transform.position, transform.rotation);

        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(_damage);
            if (character._isFacingRight)
            {
                character._rigidBody.AddForce(new Vector2(-2f, 5f), ForceMode2D.Impulse);
            }
            else if (character._isFacingLeft)
            {
                character._rigidBody.AddForce(new Vector2(2f, 5f), ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
