using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public virtual void GetCollected()
    {

        Destroy(gameObject);
    }

    public void MoveTowardsPlayer(Transform target, float moveSpeed)
    {
        StartCoroutine(MoveToTarget(target, moveSpeed));
    }

    public void StopMovingTowardsPlayer()
    {
        StopAllCoroutines();
    }

    private IEnumerator MoveToTarget(Transform target, float moveSpeed)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.01)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
