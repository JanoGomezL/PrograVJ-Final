using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AudioClip[] attackSounds; // Array para almacenar los sonidos de ataque
    private AudioSource audioSource; // Componente AudioSource
    public int damage = 10; // Daño que el enemigo inflige al jugador por ataque

    [Header("Jugador")]
    public GameObject player; // Referencia al jugador

    private void Start()
    {
        // Obtener o agregar el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Si no se ha asignado el jugador manualmente, buscarlo por su nombre
        if (player == null)
        {
            player = GameObject.Find("player");
        }

        if (player == null)
        {
            Debug.LogWarning("No se encontró un objeto llamado 'player'. Asegúrate de que existe en la jerarquía y tiene el script 'Health'.");
        }
    }

    public void PerformAttack(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("No se puede realizar el ataque porque el objetivo no está asignado.");
            return;
        }

        Debug.Log("Atacando al jugador...");

        // Reproducir un sonido aleatorio de ataque
        if (attackSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, attackSounds.Length);
            audioSource.PlayOneShot(attackSounds[randomIndex]);
        }

        // Aplicar daño al objetivo
        ApplyDamageToPlayer(target);
    }

    private void ApplyDamageToPlayer(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("El objetivo no está asignado. No se puede aplicar daño.");
            return;
        }

        // Buscar el componente Health en el objetivo
        Health playerHealth = target.GetComponent<Health>();

        if (playerHealth != null)
        {
            Debug.Log("Aplicando daño al objetivo...");
            playerHealth.TakeDamage(damage); // Aplica el daño al objetivo
        }
        else
        {
            Debug.LogWarning("El objetivo no tiene un componente Health asignado.");
        }
    }
}
