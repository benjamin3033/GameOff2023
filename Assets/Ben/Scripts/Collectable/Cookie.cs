using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : Collectable
{
    public int value = 1;

    public override void GetCollected()
    {
        GameController.Instance.IncreaseCookies(value);

        base.GetCollected();
    }
}
