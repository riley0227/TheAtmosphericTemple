using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public PistolVisibility pistolVisibility;
    public AudioSource shootSound;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && levelManager != null && !levelManager.IsGameEnded)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (shootSound != null)
        {
            shootSound.Play();
        }

        pistolVisibility.ShowPistol(.5f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(firePoint.position).z;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = (targetPos - firePoint.position).normalized;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;
    }
}
