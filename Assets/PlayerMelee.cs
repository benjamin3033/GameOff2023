using System.Collections;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public static PlayerMelee Instance;

    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] PlayerResizing playerResizing;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] GameObject SmashEffect;
    [SerializeField] GameObject StompEffect;

    private readonly string[] attackAnimationNames = { "SmashAttack", "StompAttack" };

    private void OnEnable()
    {
        playerResizing.IsBig += SizeChanged;
        Instance = this;
    }

    private void OnDisable()
    {
        playerResizing.IsBig -= SizeChanged;
    }

    private void SizeChanged(bool IsBig)
    {
        if (IsBig)
        {
            StartCoroutine(AttackCooldown());
            StartCoroutine(RandomAttacks());
            playerAnimation.ChangeLayer(2, 0);
            playerShooting.ToggleWeaponVisibility(false);
            playerMovement.ChangeMovementSpeed(1);
            GameController.Instance.CanTakeDamage = false;
        }
        else
        {
            StopAllCoroutines();
            playerAnimation.ChangeLayer(2, 1);
            playerShooting.ToggleWeaponVisibility(true);
            playerMovement.ChangeMovementSpeed(5);
            GameController.Instance.CanTakeDamage = true;
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(10);
        playerResizing.ResizePlayer();
    }

    IEnumerator RandomAttacks()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));

        string randomAttack = attackAnimationNames[Random.Range(0, attackAnimationNames.Length)];

        playerAnimation.TriggerAnimation(randomAttack);

        StartCoroutine(RandomAttacks());
    }

    public void SmashAttack()
    {
        GameObject effect = Instantiate(SmashEffect);
        effect.transform.position = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(5);
                enemy.LastHitPosition = transform.position;
            }
        }
    }

    public void StompAttack()
    {
        GameObject effect = Instantiate(StompEffect);
        effect.transform.position = transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(5);
                enemy.LastHitPosition = transform.position;
            }
        }
    }

}
