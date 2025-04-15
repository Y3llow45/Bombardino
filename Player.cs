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
    private bool canMove = true;
    private Animator animator;
    private bool isMainPlayer = false;
    private Vector3 initialOffset;
    private Transform mainPlayerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isMainPlayer = gameObject.name != "SoldierClone";

        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            if (player.isMainPlayer)
            {
                mainPlayerTransform = player.transform;
                break;
            }
        }

        if (!isMainPlayer)
        {
            initialOffset = transform.position - mainPlayerTransform.position;
        }
    }

    void Update()
    {
        if (!canMove) return;
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (fireRate * fireRateModifier);
        }
    }

    public void StopMovement()
    {
        canMove = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        Vector3 forwardMovement = Vector3.right * forwardSpeed * Time.fixedDeltaTime;

        if (isMainPlayer)
        {
            float moveX = 0f;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                moveX = -((touch.position.x - Screen.width / 2f) / (Screen.width / 2f));
                moveX *= 0.7f;
            }
            Vector3 sideMovement = Vector3.forward * moveX * sideSpeed * Time.fixedDeltaTime;

            PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();
            float minOffsetZ = 0f;
            float maxOffsetZ = 0f;
            foreach (var player in allPlayers)
            {
                if (!player.isMainPlayer)
                {
                    float offsetZ = player.initialOffset.z;
                    if (offsetZ < minOffsetZ) minOffsetZ = offsetZ;
                    if (offsetZ > maxOffsetZ) maxOffsetZ = offsetZ;
                }
            }

            float zMin = -8f - minOffsetZ;
            float zMax = 8f - maxOffsetZ;
            Vector3 newPosition = rb.position + forwardMovement + sideMovement;
            newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);
            rb.MovePosition(newPosition);
        }
        else
        {
            Vector3 targetPosition = mainPlayerTransform.position + initialOffset;
            targetPosition.x = rb.position.x + forwardSpeed * Time.fixedDeltaTime;
            targetPosition.z = Mathf.Clamp(targetPosition.z, -8f, 8f);
            rb.MovePosition(targetPosition);
        }
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