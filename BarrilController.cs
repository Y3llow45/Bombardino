using UnityEngine;
using TMPro;

public class BarrelController : MonoBehaviour
{
    public int minHits = 10;
    public int maxHits = 100;
    public float maxDistance = 100f;
    public TextMeshPro hitsRemainingText;

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

    public void RegisterHit()
    {
        Debug.Log("Barrel hit via raycast");
        currentHits++;
        UpdateHitsText();
        if (currentHits >= hitsRequired)
        {
            PlayerController.IncreaseFireRate();
            Debug.Log("Barrel destroyed");
            Destroy(gameObject);
        }
    }

    public void UpdateHitsText()
    {
        if (hitsRemainingText != null)
        {
            int hitsLeft = Mathf.Max(0, hitsRequired - currentHits);
            hitsRemainingText.text = "" + hitsLeft;
        }
    }
}