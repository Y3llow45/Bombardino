using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed = 5f;
    public GameController gameController;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name == "SoldierClone")
        {
            Debug.Log("Zombie hit player/soldier! Game Over!");
            gameController.GameOver();
        }
    }
}