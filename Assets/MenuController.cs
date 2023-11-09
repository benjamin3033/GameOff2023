using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    

    public void PlayButton()
    {
        GameController.Instance.StartCoroutine(GameController.Instance.StartLevel());
    }
}
