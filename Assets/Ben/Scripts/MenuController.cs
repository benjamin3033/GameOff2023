using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    

    public void PlayButton()
    {
        GameController.Instance.StartCoroutine(GameController.Instance.StartLevel());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
