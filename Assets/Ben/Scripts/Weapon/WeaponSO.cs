using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static WeaponSO;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    [Header("Prefabs")]
    public Projectile projectile;
    public GameObject projectileTrail;
    public GameObject projectileCollision;

    [Header("Stats")]
    
    public float ShotDelay = 2f;
    public int ShotDamage = 1;
    public float ShotLifeTime = 2f;
    public float ProjectileSpeed = 10f;

    [Header("Extras")]
    [Range(0,1)]
    public float CameraShakeAmount = 1;

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
