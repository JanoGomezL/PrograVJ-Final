using UnityEngine;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    private BaseNode behaviorTree;
    private EnemyMovement movement;
    private EnemyAttack attack;
    private EnemyDetection detection;
    private EnemyHealth health;

    public Transform player; // Referencia al jugador
    public Transform[] waypoints; // Puntos de patrullaje
    private int currentWaypoint = 0;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        detection = GetComponent<EnemyDetection>();
        health = GetComponent<EnemyHealth>();

        if (movement == null || attack == null || detection == null || health == null)
        {
            Debug.LogError("[EnemyBehavior] Faltan referencias a Movement, Attack, Detection o Health.");
            return;
        }

        var patrolAction = new ActionNode(() =>
        {
            movement.Patrol(waypoints, ref currentWaypoint);
            return true; // Siempre tiene éxito
        });

        var chaseAction = new ActionNode(() =>
        {
            movement.Chase(player);
            return true; // Siempre tiene éxito
        });

        var attackAction = new ActionNode(() =>
        {
            attack.PerformAttack(player);
            return true; // Siempre tiene éxito
        });

        behaviorTree = new SelectorNode(new List<BaseNode>
        {
            new SequenceNode(new List<BaseNode>
            {
                new ActionNode(() =>
                {
                    return detection.IsPlayerDetected(player);
                }),
                new SelectorNode(new List<BaseNode>
                {
                    new SequenceNode(new List<BaseNode>
                    {
                        new ActionNode(() =>
                        {
                            return detection.IsPlayerInAttackRange(player);
                        }),
                        attackAction
                    }),
                    chaseAction
                })
            }),
            patrolAction
        });
    }

    private void Update()
    {
        if (behaviorTree != null && health.CurrentHealth > 0)
        {
            behaviorTree.Execute(); // Ejecutar si está vivo
        }
    }
}
