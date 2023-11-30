using Cinemachine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineImpulseSource shake;
    [SerializeField] Renderer projectileRenderer;
    private WeaponSO weapon;

    private int PierceHealth;

    private void OnTriggerEnter(Collider other)
    {
        switch (weapon.chosenProjectileType)
        {
            case WeaponSO.ProjectileType.Basic:
                {
                    if (other.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.TakeDamage(weapon.ShotDamage);
                        DestroyProjectile(0);
                        CollisionParticle();
                    }
                }
                break;

            case WeaponSO.ProjectileType.Explosive:
                {
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, weapon.ExplosiveRadius);

                    foreach (var hitCollider in hitColliders)
                    {
                        if (hitCollider.TryGetComponent<Enemy>(out Enemy enemy))
                        {
                            enemy.TakeDamage(weapon.ShotDamage);
                            enemy.LastHitPosition = transform.position;
                        }
                    }
                    DestroyProjectile(0);
                    CollisionParticle();
                }
                break;

            case WeaponSO.ProjectileType.Piercing:
                {
                    if (other.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.TakeDamage(weapon.ShotDamage);
                        CollisionParticle();

                        Debug.Log(PierceHealth);

                        PierceHealth--;

                        if (PierceHealth <= 0)
                        {
                            DestroyProjectile(0);
                        }
                    }
                }
                break;
        }
        
        if (other.TryGetComponent<Enemy>(out Enemy test)) { return; }
        
        DestroyProjectile(0);
        CollisionParticle();
    }

    public void SetWeapon(WeaponSO weaponSO)
    {
        weapon = weaponSO;

        if(weapon.chosenProjectileType == WeaponSO.ProjectileType.Piercing)
        {
            PierceHealth = weapon.PierceAmount;
        }
    }

    public void SendProjectileInDirection(Vector3 direction)
    {
        transform.forward = direction;
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
