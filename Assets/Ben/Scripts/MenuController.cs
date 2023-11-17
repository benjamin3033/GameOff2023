using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Button QuitButton;

    [SerializeField] GameObject MainMenuCamera;
    [SerializeField] GameObject PlayMenuCamera;

    [SerializeField] GameObject WeaponCanvas;
    [SerializeField] GameObject LevelCanvas;

    private void Start()
    {
        PlayButton.onClick.AddListener(PressedPlayButton);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void PressedPlayButton()
    {
        MainMenuCamera.SetActive(false);
        PlayMenuCamera.SetActive(true);
    }

    public void ChooseWeapon(WeaponSO weapon)
    {
        GameController.Instance.ChooseWeapon(weapon);
        SwitchToLevelSelection();
    }

    public void SwitchToLevelSelection()
    {
        WeaponCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
    }

    public void ChooseLevel(LevelSO level)
    {
        GameController.Instance.ChooseLevel(level);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    
}
