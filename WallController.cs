using UnityEngine;
using TMPro;

public class WallController : MonoBehaviour
{
    public TextMeshPro numberText;
    public TextMeshPro signText;
    public Material wallGoodMaterial;
    public Material wallBadMaterial;

    private int cloneAmount;
    private bool hasPassed = false;

    void Start()
    {
        int sign = Random.Range(0, 2) == 0 ? 1 : -1;
        int number = Random.Range(1, 6);
        cloneAmount = sign * number;

        signText.text = sign > 0 ? "+" : "-";
        numberText.text = number.ToString();

        // set color
        Renderer wallRenderer = GetComponent<Renderer>();
        wallRenderer.material = sign > 0 ? wallGoodMaterial : wallBadMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPassed)
        {
            hasPassed = true;
            Debug.Log($"Wall hit: {cloneAmount}");
        }
    }
}