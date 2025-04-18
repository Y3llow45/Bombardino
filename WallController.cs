using UnityEngine;
using TMPro;

public class WallController : MonoBehaviour
{
    public TextMeshPro numberText;
    public TextMeshPro signText;
    public Material wallGoodMaterial;
    public Material wallBadMaterial;

    private bool triggered = false;
    private int cloneAmount;
    private static int wallIndex = 0;

    // Hardcoded number and sign for each wall
    private static readonly (int number, int sign)[] wallValues = new (int, int)[]
    {
        (2, 1),  // Wall 0: +2
        (1, -1), // Wall 1: -1
        (3, 1),  // Wall 2: +3
        (2, -1), // Wall 3: -2
        (4, 1),  // Wall 4: +4
        (3, -1)  // Wall 5: -3
    };

    void Start()
    {
        int currentIndex = wallIndex++;
        // Get number and sign from array, default to last value if index exceeds array
        (int number, int sign) = currentIndex < wallValues.Length ? wallValues[currentIndex] : wallValues[wallValues.Length - 1];

        cloneAmount = sign * number;

        signText.text = sign > 0 ? "+" : "-";
        numberText.text = number.ToString();
        GetComponent<Renderer>().material = sign > 0 ? wallGoodMaterial : wallBadMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;
        ApplyMathEffect();
    }

    private void ApplyMathEffect()
    {
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            int number = int.Parse(numberText.text);
            if (signText.text == "+")
                gameController.SpawnSoldiers(number);
            else
                gameController.RemoveSoldiers(number);
        }
    }

    void OnDestroy()
    {
        if (wallIndex == FindObjectsOfType<WallController>().Length)
            wallIndex = 0;
    }
}