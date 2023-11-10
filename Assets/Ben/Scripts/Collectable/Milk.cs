using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : Collectable
{
    public float MilkValue;

    public override void GetCollected()
    {
        GameController.Instance.IncreaseMilk(MilkValue);

        base.GetCollected();
    }
}
