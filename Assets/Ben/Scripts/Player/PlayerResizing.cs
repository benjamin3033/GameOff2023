using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResizing : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] float ExplosionForce = 10;
    [SerializeField] float UpwardMultiplyer = 2f;
    [SerializeField] Vector3 SlimScale = Vector3.one;
    [SerializeField] Vector3 FatScale = new(2, 2, 2);

    [SerializeField] private bool ScaleIsSlim = true;

    private void OnEnable()
    {
        inputHandler.resize += ResizeInput;
    }

    private void OnDisable()
    {
        inputHandler.resize -= ResizeInput;
    }

    private void ResizeInput()
    {
        if (GameController.Instance.CurrentHealth < 100) { return; }

        ResizePlayer();
    }

    public void ResizePlayer()
    {
        ScaleIsSlim = !ScaleIsSlim;
        transform.localScale = ScaleIsSlim ? SlimScale : FatScale;

        if(!ScaleIsSlim)
        {
            Vector3 playerPosition = transform.position;

            RaycastHit[] hits = Physics.SphereCastAll(playerPosition, 5, Vector3.up, 0);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
                {
                    enemyComponent.ExplodeAwayFromPlayer(transform.position, ExplosionForce, UpwardMultiplyer, 5);
                }
            }
        }
    }
}
