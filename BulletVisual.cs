using UnityEngine;

public class BulletVisual : MonoBehaviour
{
    public float lifetime = 0.5f;
    private Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        Vector3 currentPos = transform.position;
        Vector3 direction = currentPos - lastPos;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(lastPos, direction.normalized, out hit, distance))
            {
                if (hit.collider.CompareTag("Barrel"))
                {
                    BarrelController barrel = hit.collider.GetComponent<BarrelController>();
                    if (barrel != null)
                    {
                        barrel.RegisterHit();
                    }
                }
                if (hit.collider.CompareTag("Zombie"))
                {
                    Debug.Log("Hit HIM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController == null)
                    {
                        Debug.LogError("GameController not found in the scene!");
                    }
                    else
                    {
                        gameController.AddScore(10);
                    }
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        lastPos = currentPos;
    }
}
