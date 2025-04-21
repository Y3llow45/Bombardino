using UnityEngine;
using TMPro;

public class WallController : MonoBehaviour
{
    public TextMeshPro wallText;
    public Material wallGoodMaterial;
    public Material wallBadMaterial;

    private bool triggered = false;

    void Start()
    {
        string text = wallText.text;

        if (text.StartsWith("+"))
        {
            GetComponent<Renderer>().material = wallGoodMaterial;
        }
        else if (text.StartsWith("-"))
        {
            GetComponent<Renderer>().material = wallBadMaterial;
        }
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
            string text = wallText.text;
            bool isPositive = text.StartsWith("+");
            int num = int.Parse(text.Substring(1));

            if (isPositive)
                gameController.SpawnSoldiers(num);
            else
                gameController.RemoveSoldiers(num);
        }
    }
}