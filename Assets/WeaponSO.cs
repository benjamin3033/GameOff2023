using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public Projectile projectile;
    public float ShotDelay = 2f;
    public int ShotDamage = 1;
    public float ShotLifeTime = 2f;
    public float ProjectileSpeed = 10f;
}
