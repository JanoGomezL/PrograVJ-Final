using UnityEngine;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour
{
    private BaseNode behaviorTree;

    private EnemyMovement movement;
    private EnemyAttack attack;
    private EnemyDetection detection;

    public Transform player; // Referencia al jugador
    public Transform[] waypoints; // Puntos de patrullaje
    private int currentWaypoint = 0;

    void Start()
    {
        // Inicializa las referencias a los componentes
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        detection = GetComponent<EnemyDetection>();

        if (movement == null || attack == null || detection == null)
        {
            Debug.LogError("[EnemyBehavior] Faltan referencias a Movement, Attack o Detection.");
            return;
        }

        // Define las acciones utilizando los scripts existentes
        var patrolAction = new ActionNode(() =>
        {
            movement.Patrol(waypoints, ref currentWaypoint);
            return true; // Siempre retorna true
        });

        var chaseAction = new ActionNode(() =>
        {
            movement.Chase(player);
            return true; // Siempre retorna true
        });

        var attackAction = new ActionNode(() =>
        {
            attack.PerformAttack(player);
            return true; // Siempre retorna true
        });

        // Construcción del árbol de comportamiento
        behaviorTree = new SelectorNode(new List<BaseNode>
        {
            new SequenceNode(new List<BaseNode>
            {
                new ActionNode(() =>
                {
                    return detection.IsPlayerDetected(player); // Verifica si el jugador está detectado
                }),
                new SelectorNode(new List<BaseNode>
                {
                    new SequenceNode(new List<BaseNode>
                    {
                        new ActionNode(() =>
                        {
                            return detection.IsPlayerInAttackRange(player); // Verifica si el jugador está en rango de ataque
                        }),
                        attackAction
                    }),
                    chaseAction // Persigue al jugador si no está en rango de ataque
                })
            }),
            patrolAction // Patrulla si no detecta al jugador
        });
    }

    void Update()
    {
        if (behaviorTree != null)
        {
            behaviorTree.Execute(); // Ejecuta el árbol de comportamiento
        }
        else
        {
            Debug.LogError("[EnemyBehavior] El árbol de comportamiento no está inicializado.");
        }
    }
}
