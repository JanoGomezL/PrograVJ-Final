using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Referencia al texto TMP
    public Transform weaponHoldPoint; // Punto en la Main Camera donde se sujetará el arma
    private bool isPlayerInRange = false;

    void Start()
    {
        // Asegúrate de que el mensaje esté vacío al inicio
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    // Detectar cuando el jugador entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageText != null)
            {
                messageText.text = "Presiona 'E'";
            }
        }
    }

    // Detectar cuando el jugador sale del trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (messageText != null)
            {
                messageText.text = "";
            }
        }
    }

    // Detectar interacción (tecla "E") mientras el jugador está en rango
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Jugador recogió el arma");
            if (messageText != null)
            {
                messageText.text = "";
            }

            // Mover el arma al punto de sujeción
            AttachWeaponToPlayer();
        }
    }

    // Método para mover el arma a la Main Camera
    private void AttachWeaponToPlayer()
    {
        if (weaponHoldPoint != null)
        {
            // Desactiva la física del arma
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Ajustar la posición y rotación del arma al punto de sujeción
            transform.SetParent(weaponHoldPoint);
            transform.localPosition = Vector3.zero; // Centra el arma en el punto de sujeción
            transform.localRotation = Quaternion.identity; // Resetea la rotación
            Debug.Log("Arma equipada");
        }
        else
        {
            Debug.LogError("Weapon Hold Point no está asignado.");
        }
    }
}
