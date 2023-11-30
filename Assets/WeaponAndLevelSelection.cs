using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAndLevelSelection : MonoBehaviour
{
    [SerializeField] Button LeftArrow, RightArrow, ConfirmButton;
    [SerializeField] TMP_Text Name, Description;

    [SerializeField] List<WeaponSO> weapons = new();
    [SerializeField] List<LevelSO> levels = new();

    [SerializeField] private WeaponSO currentWeaponSO;
    [SerializeField] private LevelSO currentLevelSO;

    [SerializeField] private int levelIndex;
    [SerializeField] private int weaponIndex;

    bool WeaponScreen = true;

    public void EnableScript()
    {
        LeftArrow.onClick.AddListener(OnLeftArrow);
        RightArrow.onClick.AddListener(OnRightArrow);
        ConfirmButton.onClick.AddListener(OnConfirm);

        ChangeSelectedLevelSO();
        ChangeSelectedWeaponSO();
    }

    void OnLeftArrow()
    {
        if(WeaponScreen)
        {
            weaponIndex = (weaponIndex - 1 + weapons.Count) % weapons.Count;
            ChangeSelectedWeaponSO();
        }
        else
        {
            levelIndex = (levelIndex - 1 + levels.Count) % levels.Count;
            ChangeSelectedLevelSO();
        }
    }

    void OnRightArrow()
    {
        if (WeaponScreen)
        {
            weaponIndex = (weaponIndex + 1) % weapons.Count;
            ChangeSelectedWeaponSO();
        }
        else
        {
            levelIndex = (levelIndex + 1) % levels.Count;
            ChangeSelectedLevelSO();
        }
    }

    void OnConfirm()
    {
        if (WeaponScreen)
        {
            // Change to level selection screen
            ChangeSelectedLevelSO();
            WeaponScreen = false;
        }
        else
        {
            // Play game
            GameController.Instance.ChooseLevel(currentLevelSO);
        }
    }

    void ChangeSelectedWeaponSO()
    {
        currentWeaponSO = weapons[weaponIndex];
        Name.text = currentWeaponSO.Name;
        Description.text = currentWeaponSO.Description;
        GameController.Instance.ChooseWeapon(currentWeaponSO);
    }

    void ChangeSelectedLevelSO()
    {
        currentLevelSO = levels[levelIndex];
        Name.text = currentLevelSO.Name;
        Description.text = currentLevelSO.Description;
    }

}
