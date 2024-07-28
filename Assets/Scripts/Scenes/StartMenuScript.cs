using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScript : MonoBehaviour
{
    public GameController gameController;
    public ManageScene manageScene;

    void Awake()
    {
        manageScene = FindAnyObjectByType<ManageScene>();
    }

    public void InitializeScene(GameController gameController)
    {
        this.gameController = gameController;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseStartMenu()
    {
        manageScene.CloseScene("StartScene");
    }
}
