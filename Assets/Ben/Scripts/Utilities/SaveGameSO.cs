using BayatGames.SaveGameFree;
using UnityEngine;

[CreateAssetMenu()]
public class SaveGameSO : ScriptableObject
{
    public int MaxHealthPurchased = -1;

    public void Save()
    {
        SaveGame.Save<int>("MaxHealthPurchaed", MaxHealthPurchased);
    }

    public void Load()
    {
        if(SaveGame.Exists("MaxHealthPurchaed"))
        {
            MaxHealthPurchased = SaveGame.Load<int>("MaxHealthPurchaed");
        }
        
    }
}
