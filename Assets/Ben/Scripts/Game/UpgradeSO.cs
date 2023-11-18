using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradeSO : ScriptableObject
{
    [Header("Stats")]
    public ShopController.Upgrades upgrade;
    public List<int> Costs = new();

    [Header("Visual")]
    public Texture2D Image;
    public string UpgradeName;
    public string LongUpgradeName;
    public string UpgradeDescription;
}
