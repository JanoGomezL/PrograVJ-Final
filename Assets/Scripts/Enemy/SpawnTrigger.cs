using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public SpawnManager spawnManager; // Referencia al SpawnManager
    public int waveSize = 5;          // Tamaño de la oleada

    public Color gizmoColor = Color.cyan; // Color del trigger para pruebas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[SpawnTrigger] El jugador activó el trigger.");
            spawnManager.SpawnWave(waveSize);
            Destroy(gameObject); // Elimina el trigger después de activarlo
        }
        else
        {
            Debug.Log($"[SpawnTrigger] Otro objeto entró al trigger: {other.name}");
        }
    }

    // Método para dibujar el gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; // Usa el color configurado
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
