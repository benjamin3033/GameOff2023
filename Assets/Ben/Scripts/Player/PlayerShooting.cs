using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] InputHandler input;
    [HideInInspector] public WeaponSO currentWeapon;

    bool canShoot = true;

    private void OnEnable()
    {
        input.shoot += ShootWeaponInput;
    }

    private void OnDisable()
    {
        input.shoot -= ShootWeaponInput;
    }

    private void ShootWeaponInput()
    {
        if (!canShoot || !GameController.Instance.CanPlayerMove) { return; }

        StartCoroutine(FireWeapon());
    }

    private IEnumerator FireWeapon()
    {
        canShoot = false;

        Projectile shot = Instantiate(currentWeapon.projectile);
        shot.SetWeapon(currentWeapon);
        shot.transform.position = transform.position + (transform.forward);
        shot.SendProjectileInDirection(transform.forward * currentWeapon.ProjectileSpeed);

        if(currentWeapon.projectileTrail != null)
        {
            Instantiate(currentWeapon.projectileTrail, shot.transform);
        }

        yield return new WaitForSeconds(currentWeapon.ShotDelay);

        canShoot = true;
    }
}
