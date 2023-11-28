using System;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    [Header("Prefabs")]
    public Projectile projectile;
    public GameObject projectileTrail;
    public GameObject projectileCollision;
    public GameObject Weapon;

    [Header("Stats")]
    
    public float ShotDelay = 2f;
    public int ShotDamage = 1;
    public float ShotLifeTime = 2f;
    public float ProjectileSpeed = 10f;

    [Header("Extras")]
    [Range(0,1)]
    public float CameraShakeAmount = 1;
    public Vector3 VisualPosition;
    public Vector3 VisualRotation;
    public Vector3 ShotStartPosition;
    public string Name;
    public string Description;

    [Header("Projectile Type")]
    public ProjectileType chosenProjectileType;

    [Header("Explosive Settings")]
    public float ExplosiveRadius;

    [Header("Piercing Settings")]
    public int PierceAmount = 20;

    public enum ProjectileType
    {
        Basic,
        Explosive,
        Piercing
    }
}
