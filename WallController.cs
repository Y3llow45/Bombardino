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
    private static bool firstIsPositive = true;

    void Start()
    {
        int currentIndex = wallIndex++;
        int totalWalls = FindObjectsOfType<WallController>().Length;

        int sign = (currentIndex == 0) ? 1 :
                  (currentIndex == 1) ? -1 :
                  Random.Range(0, 2) == 0 ? 1 : -1;

        if (currentIndex == 1 && totalWalls > 2)
        {
            firstIsPositive = Random.Range(0, 2) == 0;
            sign = firstIsPositive ? -1 : 1;
        }

        int number = Random.Range(1, 6);
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