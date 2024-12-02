using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AudioClip[] attackSounds; // Array para almacenar los sonidos de ataque
    private AudioSource audioSource; // Componente AudioSource

    private void Start()
    {
        // Obtener o agregar el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PerformAttack(Transform player)
    {
        Debug.Log("Atacando al jugador...");

        // Reproducir un sonido aleatorio de ataque
        if (attackSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, attackSounds.Length);
            audioSource.PlayOneShot(attackSounds[randomIndex]);
        }

        // Aquí puedes implementar la lógica para aplicar daño al jugador
    }
}
