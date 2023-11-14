using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Enemy enemy;
    [SerializeField] Rigidbody rb;

    [System.NonSerialized] public EnemySO enemySO;

    private void Start()
    {
        SetupNavMeshAgent();
        enemySO = enemy.enemySO;
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

    public void ExplodeAwayFromPlayer(Vector3 explosionPoint, float explosionForce, float upwardMultiplyer, float MoveAgain)
    {
        StartCoroutine(ExplodeAway(explosionPoint, explosionForce, upwardMultiplyer, MoveAgain));
    }

    IEnumerator ExplodeAway(Vector3 explosionPoint, float explosionForce, float upwardMultiplyer, float MoveAgain)
    {
        agent.enabled = false;
        rb.isKinematic = false;

        Vector3 direction = transform.position - explosionPoint;
        direction.Normalize();
        direction += Vector3.up * upwardMultiplyer;
        rb.AddForce(direction * explosionForce, ForceMode.Impulse);

        yield return new WaitForSeconds(MoveAgain);

        rb.isKinematic = true;
        agent.enabled = true;
    }
}
