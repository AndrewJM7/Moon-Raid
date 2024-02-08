using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // Updates path of bullet
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Assuming the AlienHealth script is on the same GameObject as the Alien
            AlienHealth alienHealth = other.GetComponent<AlienHealth>();

            if (alienHealth != null)
            {
                // Damage the alien
                alienHealth.TakeDamage(20);
            }

            // Destroy the bullet after hitting an enemy
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

