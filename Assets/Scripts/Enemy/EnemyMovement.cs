using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;

    public void Patrol(Transform[] waypoints, ref int currentWaypoint)
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("[EnemyMovement] No hay waypoints asignados.");
            return;
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1f)
        {
            /*Debug.Log($"[EnemyMovement] Cambiando al siguiente waypoint: {currentWaypoint}");*/
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
    }

    public void Chase(Transform player)
    {
        if (player == null)
        {
            Debug.LogError("[EnemyMovement] Jugador no asignado.");
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}

