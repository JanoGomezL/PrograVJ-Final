using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    public int CurrentHealth // Propiedad pública para acceder a la vida actual
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth; // Inicializar la vida al máximo
    }

    private void Update()
    {
        // Detecta si presionamos clic izquierdo y reduce la vida
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(10); // Reducir vida al hacer clic izquierdo
        }

        Debug.Log($"Vida actual del enemigo: {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducir la vida actual
        Debug.Log($"Enemigo recibió daño: {damage}. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // Eliminar al enemigo
        }
    }

    private void Die()
    {
        Debug.Log("Enemigo eliminado.");
        Destroy(gameObject); // Destruir el objeto del enemigo
    }
}
