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

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            ApplyMathEffect();
        }
    }

    private void ApplyMathEffect()
    {
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController == null) return;

        string sign = signText.text;
        int number = int.Parse(numberText.text);

        if (sign == "+")
        {
            gameController.SpawnSoldiers(number);
        }
        else
        {
            gameController.RemoveSoldiers(number);
        }
    }
}