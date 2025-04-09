using UnityEngine;
using TMPro;

public class BarrelController : MonoBehaviour
{
    public int minHits = 10;
    public int maxHits = 100;
    public float maxDistance = 100f;
    public TextMeshProUGUI hitsRemainingText;

    private int hitsRequired;
    private int currentHits = 0;

    void Start()
    {
        float distance = Vector3.Distance(Vector3.zero, transform.position);
        float distanceFactor = Mathf.Clamp01(distance / maxDistance);
        hitsRequired = Mathf.RoundToInt(Mathf.Lerp(minHits, maxHits, distanceFactor));
        UpdateHitsText();
        Debug.Log($"Barrel at {transform.position} requires {hitsRequired} hits");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHits++;
            UpdateHitsText();
            if (currentHits >= hitsRequired)
            {
                Destroy(gameObject);
                Debug.Log("Barrel destroyed");
            }
        }
    }

    void UpdateHitsText()
    {
        if (hitsRemainingText != null)
        {
            int hitsLeft = Mathf.Max(0, hitsRequired - currentHits);
            hitsRemainingText.text = "Hits left: " + hitsLeft;
        }
    }
}