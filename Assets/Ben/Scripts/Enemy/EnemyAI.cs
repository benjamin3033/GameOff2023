using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public EnemySO enemySO;

    private void Start()
    {
        SetupNavMeshAgent();

        UpdateDestination(Vector3.zero);
    }

    private void SetupNavMeshAgent()
    {
        // Steering
        agent.speed = enemySO.Speed;
        agent.angularSpeed = enemySO.AngularSpeed;
        agent.acceleration = enemySO.Acceleration;
        agent.stoppingDistance = enemySO.StoppingDistance;
        agent.autoBraking = enemySO.AutoBraking;

        // Obstacle Avoidance
        agent.radius = enemySO.Radius;
        agent.height = enemySO.Height;
        agent.obstacleAvoidanceType = enemySO.Quality;
        agent.avoidancePriority = enemySO.Priority;
    }

    public void UpdateDestination(Vector3 destination)
    {
        if(agent.isActiveAndEnabled)
        agent.destination = destination;
    }
}
