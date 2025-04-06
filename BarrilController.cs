using UnityEngine;

public class BarrelController : MonoBehaviour
{
    public int minHits = 10;
    public int maxHits = 100;
    public float maxDistance = 100f;

    private int hitsRequired;
    private int currentHits = 0;

    void Start()
    {
        float distance = Vector3.Distance(Vector3.zero, transform.position);
        float distanceFactor = Mathf.Clamp01(distance / maxDistance);
        hitsRequired = Mathf.RoundToInt(Mathf.Lerp(minHits, maxHits, distanceFactor));
        Debug.Log($"Barrel at {transform.position} requires {hitsRequired} hits");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHits++;
            if (currentHits >= hitsRequired)
            {
                Destroy(gameObject);
                Debug.Log("Barrel destroyed");
            }
        }
    }
}