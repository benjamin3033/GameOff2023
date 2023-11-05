using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] InputHandler input;
    [SerializeField] WeaponSO weapon;
    [SerializeField] Transform PlayerVisual;

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
        if (!canShoot) { return; }

        StartCoroutine(FireWeapon());
    }

    private IEnumerator FireWeapon()
    {
        canShoot = false;

        Projectile shot = Instantiate(weapon.projectile);
        shot.transform.position = PlayerVisual.localPosition + (PlayerVisual.forward);
        shot.SendProjectileInDirection(transform.forward * weapon.ProjectileSpeed);


        yield return new WaitForSeconds(weapon.ShotDelay);

        canShoot = true;
    }
}
