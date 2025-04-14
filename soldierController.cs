using UnityEngine;

public class SoldierController : MonoBehaviour
{
    public Transform mainPlayer;
    public int index;
    public float spacing = 1.0f;
    public int columns = 4;
    public float speed = 5.0f;

    void FixedUpdate()
    {
        if (mainPlayer == null) return;

        int row = index / columns;
        int col = index % columns;
        float offsetX = -row * spacing;
        float offsetZ = (col - (columns / 2f - 0.5f)) * spacing;
        Vector3 offset = new Vector3(offsetX, 0, offsetZ);
        Vector3 targetPosition = mainPlayer.position + offset;

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}