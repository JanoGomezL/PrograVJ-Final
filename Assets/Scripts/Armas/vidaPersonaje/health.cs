using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Configuración de la Vida")]
    public int maxHealth = 100; // Vida máxima del personaje
    private int currentHealth;  // Vida actual del personaje

    [Header("Interfaz de Usuario")]
    public Slider healthBar;    // Barra de vida
    public Text healthText;     // Texto de vida (opcional)

    [Header("Efectos")]
    public GameObject deathEffect; // Efecto al morir

    private RectTransform healthBarRect; // Referencia al RectTransform del Slider

    void Start()
    {
        // Inicializar la vida del personaje
        currentHealth = maxHealth;

        // Configurar la posición inicial de la barra de vida
        if (healthBar != null)
        {
            healthBarRect = healthBar.GetComponent<RectTransform>();
            PositionHealthBar();
        }

        // Actualizar UI inicial
        UpdateHealthUI();
    }

    void Update()
    {
        // Asegurar que la barra de vida siga a la cámara principal
        if (healthBarRect != null)
        {
            PositionHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"Daño recibido: {damage}. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

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

    private void PositionHealthBar()
    {
        // Posicionar la barra de vida en la parte inferior de la cámara
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 bottomPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, mainCamera.nearClipPlane + 1));
            healthBarRect.position = mainCamera.WorldToScreenPoint(bottomPosition);
        }
    }
}
