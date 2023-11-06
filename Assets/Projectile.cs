using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private WeaponSO weapon;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            enemy.TakeDamage(weapon.ShotDamage);
            DestroyProjectile(0);
        }

        DestroyProjectile(0);
    }

    public void SetWeapon(WeaponSO weaponSO)
    {
        weapon = weaponSO;
    }

    public void SendProjectileInDirection(Vector3 direction)
    {
        rb.AddForce(direction);
        DestroyProjectile(weapon.ShotLifeTime);
    }

    private void DestroyProjectile(float time)
    {
        Destroy(gameObject, time);
    }
}
