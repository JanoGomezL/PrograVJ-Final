using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Configuración de la Vida")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Interfaz de Usuario")]
    public Image vidaPlayer;
    public TextMeshProUGUI vidaTMP;

    [Header("Efectos")]
    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        ActualizarUIPersonaje();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Heal(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Daño recibido: {damage}. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        ActualizarUIPersonaje();
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Vida curada: {healAmount}. Vida actual: {currentHealth}");

        ActualizarUIPersonaje();
    }

    private void Die()
    {
        Debug.Log("El personaje ha muerto.");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        SceneManager.LoadScene("GameOver");
    }

    private void ActualizarUIPersonaje()
    {
        if (vidaPlayer != null && vidaTMP != null)
        {
            vidaPlayer.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            vidaTMP.text = $"{currentHealth}/{maxHealth}";

            Debug.Log($"Actualizando barra de vida: {currentHealth}/{maxHealth}");
        }
        else
        {
            Debug.LogWarning("No se pueden actualizar los elementos de UI porque vidaPlayer o vidaTMP no están asignados.");
        }
    }
}

