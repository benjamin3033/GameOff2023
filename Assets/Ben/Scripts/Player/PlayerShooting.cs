using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] InputHandler input;
    [HideInInspector] public WeaponSO currentWeapon;
    [SerializeField] Transform WeaponHoldPoint;
    [SerializeField] PlayerAnimation playerAnimation;

    [SerializeField] Transform ShotStartTransform;

    bool canShoot = true;
    bool isBig = false;

    GameObject weaponVisual;

    private void OnEnable()
    {
        input.shoot += ShootWeaponInput;
    }

    private void OnDisable()
    {
        input.shoot -= ShootWeaponInput;
    }

    public void ToggleWeaponVisibility(bool turnOn)
    {
        weaponVisual.SetActive(turnOn);
        isBig = !turnOn;
    }

    public void ChangeWeapon(WeaponSO weapon)
    {
        Destroy(weaponVisual);
        weaponVisual = Instantiate(weapon.Weapon, WeaponHoldPoint);
        ShotStartTransform.localPosition = weapon.ShotStartPosition;

        weaponVisual.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        weaponVisual.transform.localPosition = weapon.VisualPosition;
        weaponVisual.transform.localEulerAngles = weapon.VisualRotation;

        playerAnimation.HoldGun();
        currentWeapon = weapon;
    }

    private void ShootWeaponInput()
    {
        if (!canShoot || !GameController.Instance.CanPlayerMove || isBig) { return; }

        StartCoroutine(FireWeapon());
    }

    private IEnumerator FireWeapon()
    {
        canShoot = false;

        Projectile shot = Instantiate(currentWeapon.projectile);
        shot.SetWeapon(currentWeapon);
        shot.transform.position = ShotStartTransform.position;

        shot.SendProjectileInDirection(ShotStartTransform.forward * currentWeapon.ProjectileSpeed);

        if(currentWeapon.projectileTrail != null)
        {
            Instantiate(currentWeapon.projectileTrail, shot.transform);
        }

        yield return new WaitForSeconds(currentWeapon.ShotDelay);

        canShoot = true;
    }
}
