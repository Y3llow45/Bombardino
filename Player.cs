using UnityEngine;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float sideSpeed = 5f;
    public float fireRate = 0.5f;
    public float bulletSpeed = 20f;
    private float nextFireTime = 0f;
    private Rigidbody rb;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public static float fireRateModifier = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (fireRate * fireRateModifier);
        }
    }

    void FixedUpdate()
    {
        Vector3 forwardMovement = Vector3.right * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        float moveX = 0f;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            moveX = -((touch.position.x - Screen.width / 2f) / (Screen.width / 2f));

            moveX *= 0.7f;
        }
        Vector3 sideMovement = Vector3.forward * moveX * sideSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + sideMovement);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = transform.forward * bulletSpeed;
    }

    public static void IncreaseFireRate()
    {
        fireRateModifier *= 0.9f;
        Debug.Log("New fire rate modifier: " + fireRateModifier);
    }
}