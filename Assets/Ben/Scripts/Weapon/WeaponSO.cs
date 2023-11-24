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

    [Header("Projectile Type")]
    public ProjectileType chosenProjectileType;

    [Header("Explosive Settings")]
    public float ExplosiveRadius;

    public enum ProjectileType
    {
        Basic,
        Explosive
    }
}
