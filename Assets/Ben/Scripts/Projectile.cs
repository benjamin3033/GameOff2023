using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineImpulseSource shake;
    [SerializeField] Renderer projectileRenderer;
    private WeaponSO weapon;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            enemy.TakeDamage(weapon.ShotDamage);
            DestroyProjectile(0);

            CollisionParticle();
        }

        DestroyProjectile(0);
        CollisionParticle();
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

    private void CollisionParticle()
    {
        if (weapon.projectileCollision != null && SeenByCamera())
        {
            GameObject particle = Instantiate(weapon.projectileCollision);
            particle.transform.position = transform.position;
            shake.GenerateImpulse(weapon.CameraShakeAmount);
        }
    }

    private bool SeenByCamera()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(transform.position);

        if(point.x > 0 && point.x < 1 && point.y > 0 && point.y < 1 && point.z >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DestroyProjectile(float time)
    {
        
        
        Destroy(gameObject, time);
    }
}
