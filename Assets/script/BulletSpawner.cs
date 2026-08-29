using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float spawnInterval = 2.0f;
    [SerializeField] private float bulletSpeed = 10.0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // Åyí«â¡Åzê∂ê¨Ç≥ÇÍÇΩíeÉIÉuÉWÉFÉNÉgÇ3ïbå„Ç…é©ìÆÇ≈çÌèúÇ∑ÇÈ
            Destroy(bullet, 1.0f);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
            }
        }
    }
}
