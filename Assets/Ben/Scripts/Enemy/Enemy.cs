using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float Health = 1;
    [SerializeField] EnemySO enemySO;
    [SerializeField] Transform Visual;
    [SerializeField] Rigidbody rb;
    [SerializeField] NavMeshAgent agent;
    public Action<EnemyAI> EnemyDied;

    private void Start()
    {
        Health = enemySO.Health;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0) { KillEnemy(); }
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

    private void KillEnemy()
    {
        EnemyDied?.Invoke(GetComponent<EnemyAI>());
        Visual.localRotation = Quaternion.Euler(0, -90, 90);
        Visual.parent = null;

        GameController.Instance.SpawnCookie(transform.position);
        GameController.Instance.SpawnMilk(transform.position);

        Destroy(gameObject);
    }
}
