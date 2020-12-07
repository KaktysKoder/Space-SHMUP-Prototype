using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    private void Update()
    {
        // Если снаряд вышел за верхнюю границу экрана, уничтожить его.
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }
}