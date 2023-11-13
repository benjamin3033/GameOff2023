using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        GameController.Instance.ChangeHealth(-amount);
    }

    public void HealHealth(int amount)
    {
        GameController.Instance.ChangeHealth(amount);
    }
}
