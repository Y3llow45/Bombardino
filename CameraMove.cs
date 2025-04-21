using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(-12, 5, 0);

    void Update()
    {
        transform.position = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            player.position.z * 0.6f + offset.z
        );
    }
}
