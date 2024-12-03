using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [Header("Configuración de la Vida")]
    public float maxHealth = 100f; // Vida máxima del personaje
    private float currentHealth;  // Vida actual del personaje

    [Header("Interfaz de Usuario")]
    public Image vidaPlayer;        // Barra de vida (Image con fillAmount)
    public TextMeshProUGUI vidaTMP; // Texto de vida (TMP)

    [Header("Efectos")]
    public GameObject deathEffect; // Efecto al morir

    void Start()
    {
        // Inicializar la vida del personaje
        currentHealth = maxHealth;

        // Actualizar UI inicial
        ActualizarUIPersonaje();
    }

    void Update()
    {
        // Probar reducción de vida con la tecla "U"
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(10f); // Reducir la vida en 10
        }

        // Probar aumento de vida con la tecla "Y"
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Heal(10f); // Aumentar la vida en 10
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurar que la vida no sea negativa

        Debug.Log($"Daño recibido: {damage}. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        // Actualizar la barra de vida y el texto
        ActualizarUIPersonaje();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegurar que no supere la vida máxima

        Debug.Log($"Vida curada: {healAmount}. Vida actual: {currentHealth}");

        // Actualizar la barra de vida y el texto
        ActualizarUIPersonaje();
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

    private void ActualizarUIPersonaje()
    {
        if (vidaPlayer != null && vidaTMP != null)
        {
            // Actualizar el fillAmount de la barra de vida
            vidaPlayer.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            
            // Actualizar el texto de vida
            vidaTMP.text = $"{currentHealth}/{maxHealth}";

            Debug.Log($"Actualizando barra de vida: {currentHealth}/{maxHealth}");
        }
        else
        {
            Debug.LogWarning("No se pueden actualizar los elementos de UI porque vidaPlayer o vidaTMP no están asignados.");
        }
    }
}
