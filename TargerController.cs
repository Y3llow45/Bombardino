using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
    public float speed = 5f;
    public float moveSpeed = 3f;
    public float moveRange = 4.2f;
    private bool startMoving = false;

    private float initialZ;

    void Start()
    {
        initialZ = transform.position.z;
        StartCoroutine(WaitBeforeMoving());
    }

    void Update()
    {
        if (startMoving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            float moveZ = -Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float newZ = Mathf.Clamp(transform.position.z + moveZ, initialZ - moveRange, initialZ + moveRange);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(1.4f);
        startMoving = true;
    }
}