using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Enemy enemy;
    [SerializeField] Rigidbody rb;
    [SerializeField] EnemyAttacking enemyAttacking;

    [HideInInspector] public EnemySO enemySO;
    

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

    public void ExplodeAwayFromPlayer(Vector3 explosionPoint, float explosionForce, float upwardMultiplyer, float MoveAgain, bool death)
    {
        if(death)
        {
            StartCoroutine(Ragdoll(explosionPoint, explosionForce, upwardMultiplyer));
        }
        else
        {
            StartCoroutine(ExplodeAway(explosionPoint, explosionForce, upwardMultiplyer, MoveAgain));
        }
    }

    IEnumerator Ragdoll(Vector3 explosionPoint, float explosionForce, float upwardMultiplyer)
    {
        agent.enabled = false;
        rb.isKinematic = false;

        Vector3 direction = transform.position - explosionPoint;
        direction.Normalize();
        direction += Vector3.up * upwardMultiplyer;
        rb.AddForce(direction * explosionForce, ForceMode.Impulse);

        gameObject.layer = 14;

        Destroy(enemyAttacking.animator);
        Destroy(enemyAttacking);

        GameObject body = Instantiate(enemySO.Corpse, transform);

        MeshRenderer blood = Instantiate(enemy.BloodPrefab);
        blood.material.SetTexture("_Blood_Image", enemy.bloods[UnityEngine.Random.Range(0, enemy.bloods.Count)]);
        blood.transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);

        yield return new WaitForSeconds(1.5f);

        body.transform.parent = null;

        //body.transform.SetPositionAndRotation(new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);

        body.transform.position = new Vector3(body.transform.position.x, 0, body.transform.position.z);

        

        GameController.Instance.EnemyDied(transform.position);

        Destroy(gameObject);


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
