using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}
