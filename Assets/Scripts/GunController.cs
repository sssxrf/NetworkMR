using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    [Header("Bullet Settings")]
    // Bullet prefab must have a Rigidbody component
    public GameObject bulletPrefab;
    // Transform that represents where bullets spawn (e.g., an empty game object at the gun muzzle)
    public Transform bulletSpawnPoint;
    // How fast the bullet will travel
    public float bulletSpeed = 20f;
    // How many seconds before the bullet is automatically destroyed
    public float bulletLifeTime = 2f;

    [Header("Ammo Settings")]
    // Maximum number of bullets per magazine
    public int maxBullets = 30;
    // Current number of bullets available
    private int currentBullets;

    [Header("UI ")]
    // Reference to a UI Text element to display ammo count (set in Inspector)
    public TMP_Text ammoCountUI;

    // Initialization
    void Start()
    {
        currentBullets = maxBullets;
        UpdateAmmoUI();
    }

    // Called once per frame
    void Update()
    {
        //// Check if the player presses the left mouse button (or Fire1) to shoot
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Shoot();
        //}

        //// Check if the player presses the R key to reload
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Reload();
        //}
    }

    // Method to instantiate and shoot a bullet
    public void Shoot()
    {
        if (currentBullets > 0)
        {
            // Instantiate the bullet at the spawn point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Try to get the Rigidbody component from the bullet prefab
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Set the bullet's velocity in the forward direction of the spawn point
                rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }

            // Decrease the bullet count
            currentBullets--;
            UpdateAmmoUI();

            // Destroy the bullet after 'bulletLifeTime' seconds
            Destroy(bullet, bulletLifeTime);
        }
        else
        {
            // Optionally, output a message or play a sound if there's no ammo left
            Debug.Log("No ammo! Reload your gun!");
        }
    }

    // Method to update the ammo count on screen (if using a UI Text element)
    private void UpdateAmmoUI()
    {
        if (ammoCountUI != null)
        {
            ammoCountUI.text = currentBullets.ToString();
        }
    }

    // Method to reload the gun (resets bullet count to max)
    public void Reload()
    {
        currentBullets = maxBullets;
        UpdateAmmoUI();
        Debug.Log("Gun reloaded!");
    }
}
