using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [Header("Configuración de la Vida")]
    public int maxHealth = 100; // Vida máxima del personaje
    private int currentHealth;  // Vida actual del personaje

    [Header("Interfaz de Usuario")]
    public Slider healthBar;    // Barra de vida (opcional)
    public Text healthText;     // Texto de vida (opcional)

    [Header("Efectos")]
    public GameObject deathEffect; // Efecto al morir

    void Start()
    {
        // Inicializar la vida del personaje
        currentHealth = maxHealth;

        // Actualizar UI inicial
        UpdateHealthUI();
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Verificar si la vida está por debajo de 0
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Actualizar la UI
        UpdateHealthUI();
    }

  
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Asegurarse de no superar la vida máxima
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Actualizar la UI
        UpdateHealthUI();
    }

   
    private void Die()
    {
        Debug.Log("El personaje ha muerto.");

        // Mostrar efecto de muerte (si está configurado)
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Desactivar el personaje o realizar alguna acción al morir
        gameObject.SetActive(false); // Desactiva al personaje
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"Vida: {currentHealth}/{maxHealth}";
        }
    }
}
