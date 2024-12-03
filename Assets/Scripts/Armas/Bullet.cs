using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;       // Velocidad del proyectil
    public float lifetime = 2f;    // Tiempo de vida del proyectil

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacto con: " + other.name);

        // Aquí puedes manejar el daño si el objeto tiene vida
        // Ejemplo:
        // Health health = other.GetComponent<Health>();
        // if (health != null) health.TakeDamage(damage);

        Destroy(gameObject);
    }
}
