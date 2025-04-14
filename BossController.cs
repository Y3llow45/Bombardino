using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public Animator animator;
    private bool isAttacking = false;
    public float attackDistance = 1.5f;

    private int shotCount = 0;
    private bool hasChangedAnimation = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Boss Animator missing!");
        if (player == null) Debug.LogError("Player not assigned!");
    }

    void Update()
    {
        if (isAttacking || player == null) return;
        transform.position += (player.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= attackDistance)
        {
            isAttacking = true;
            animator.SetTrigger("Hit");
            Debug.Log("Boss reached player, playing hit animation");

            moveSpeed = 0f;

            PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();
            foreach (PlayerController pc in allPlayers)
            {
                pc.StopMovement();
            }

            Invoke("TriggerGameOver", 1f);
        }
    }

    void TriggerGameOver()
    {
        FindObjectOfType<GameController>().GameOver();
    }

    void TriggerWin()
    {
        FindObjectOfType<GameController>().Win();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            shotCount++;
            Debug.Log("Boss shot count: " + shotCount);

            if (shotCount >= 50 && !hasChangedAnimation)
            {
                hasChangedAnimation = true;
                if (animator != null)
                {
                    animator.SetTrigger("GotShot20Times");
                    Invoke("TriggerWin", 2f);
                }
                else
                {
                    Debug.LogError("Animator is null in OnTriggerEnter");
                }
            }
        }
    }
}