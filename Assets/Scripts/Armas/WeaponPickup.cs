using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Mensaje en pantalla
    private bool isPlayerInRange = false;

    void Start()
    {
        if (messageText != null)
        {
            messageText.text = ""; // Asegurar que el mensaje esté vacío al inicio
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageText != null)
            {
                messageText.text = "Presiona E para recoger el arma";
            }
        }
    }

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

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            AttachWeaponToPlayer();
        }
    }

    private void AttachWeaponToPlayer()
{
    PlayerController player = FindObjectOfType<PlayerController>();
    if (player != null)
    {
        Weapon newWeapon = GetComponent<Weapon>();
        if (newWeapon != null)
        {
            player.EquipWeapon(newWeapon);

            // Desactiva la física del arma
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Desactivar el collider del arma
            Collider weaponCollider = GetComponent<Collider>();
            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }

            Debug.Log("Arma recogida y equipada: " + newWeapon.weaponName);
        }
        else
        {
            Debug.LogError("No se encontró un script Weapon en el objeto.");
        }
    }
    else
    {
        Debug.LogError("PlayerController no encontrado en la escena.");
    }
}

}
