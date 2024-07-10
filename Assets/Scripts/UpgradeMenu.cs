using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject upgrade;

    public void OpenMenu()
    {
        Time.timeScale = 0f; //pause the game when upgrade menu comes out
        upgrade.SetActive(true);
    }

    public void CloseMenu()
    {
        upgrade.SetActive(false);
        Time.timeScale = 1f; //unpause after upgrading
    }
}
