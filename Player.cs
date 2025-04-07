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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        // move
        Vector3 forwardMovement = Vector3.right * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // left/right
        float moveX = 0f;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            moveX = touch.position.x < Screen.width / 2f ? -1f : 1f;
        }
        Vector3 sideMovement = Vector3.forward * moveX * sideSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + sideMovement);
    }

    void Shoot()
    {
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + hit.collider.name);
            }
        }*/ 
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        //bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
        bulletRb.linearVelocity = transform.forward * bulletSpeed; // If player’s forward is along Z-axis
    }
}