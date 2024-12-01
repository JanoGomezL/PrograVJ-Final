using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f; // Rango de detección
    public float attackRange = 1.5f;  // Rango de ataque

        // Verifica si el jugador está dentro del rango de detección
    public bool IsPlayerDetected(Transform player)
    {
        if (player == null) 
        {
            Debug.LogError("[EnemyDetection] Jugador no asignado.");
            return false;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log($"[EnemyDetection] Distancia al jugador: {distance}. Rango de detección: {detectionRange}");
        return distance <= detectionRange;
    }

    public bool IsPlayerInAttackRange(Transform player)
    {
        if (player == null) 
        {
            Debug.LogError("[EnemyDetection] Jugador no asignado.");
            return false;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log($"[EnemyDetection] Distancia al jugador: {distance}. Rango de ataque: {attackRange}");
        return distance <= attackRange;
    }

    // Método para dibujar los Gizmos en el editor
    private void OnDrawGizmosSelected()
    {
        // Cambia el color para el rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Cambia el color para el rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
