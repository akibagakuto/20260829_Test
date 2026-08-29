using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = startPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
