using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;

    void Update()
    {
        transform.position += (player.position - transform.position).normalized * speed * Time.deltaTime;
    }
}
