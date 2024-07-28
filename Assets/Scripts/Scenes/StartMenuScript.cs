using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the functions of UI in the Start menu
public class StartMenuScript : MonoBehaviour
{
    public GameController gameController;
    public ManageScene manageScene;

    public DialogueScene dialogueScene;
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

    public void CloseStartMenu() //this is when player presses on start
    {
        manageScene.CloseScene("StartScene");
        dialogueScene.OpenDialogue(); //open dialogue after starting the game
    }
}
