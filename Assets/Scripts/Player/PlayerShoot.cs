using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool isShooting;

    void Update()
    {
        if (isShooting)
        {
           Shoot();           
        }
    }

    public void Shoot()
    {
        // Can't shoot when game is paused
        if (!PauseMenu.GameIsPaused)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Ensure the bullet moves in the forward direction of the firePoint
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(firePoint.forward);
            }

            Destroy(bullet, 2f);
        }
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }
}


