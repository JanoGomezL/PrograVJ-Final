using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Velocidad de la bala
    public float lifeTime = 2f; // Tiempo de vida de la bala
    public int damage = 10; // Daño que inflige la bala

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destruir la bala después de un tiempo
    }

    private void Update()
    {
        // Mover la bala hacia adelante en su orientación
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"La bala impactó con: {other.gameObject.name}");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Impacto confirmado con enemigo.");
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Aplica daño
            }

            Destroy(gameObject); // Destruir la bala tras el impacto
        }
    }
}
