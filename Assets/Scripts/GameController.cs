using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script controls the main flow of the game


public class GameController : MonoBehaviour
{
    public GameObject playerObj;
    public InputHandler inputHandler;

    private List<Weapon> weaponInventory;
    private List<item> itemInventory;
    private Character chosenCharacter;
    public DialogueScene dialogueScene;
    public DataManager dataManager;

    public PlayerInventory playerInventory;

    public Player player;

    public List<GameObject> mapPrefabs;

    public GameObject expDrop;

    public TimeTracker timeTracker;

    public ManageScene manageScene;

    public bool gameActive;
    
    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        dataManager.LoadAllData();
        player = FindObjectOfType<Player>();

        manageScene = FindAnyObjectByType<ManageScene>();
        OpenStartMenu(); //open start menu when game starts

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameActive = true;
        
        //set corresponding sprite image for chosen character class
        string spriteFilePath = Game.GetChar().spriteImage;
        Sprite playerSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
        player.GetComponent<SpriteRenderer>().sprite = playerSprite;

        player.transform.position = Vector2.zero;
        foreach(PlayerScript playerScript in player.GetComponents<PlayerScript>())
        {
            playerScript.Initialize(this);
        }
        inputHandler.SetInputReceiver(player.GetComponent<playerMovement>());
    }

    public void EndGame() //end game 
    {
        gameActive = false;
        Game.SetTime(timeTracker.endText);
        dialogueScene.OpenDialogue();
    }

    public void Surrender() //this is for when the player presss on give up 
    {
        player.Die(); //kills the player immediately as this is considered a failed attempt
    }

    public void ReOpenStartMenu() //return to start menu
    {
        Game.ResetEnemiesKilledCounters(); //reset eveything
        OpenStartMenu();
    }


    public void OpenStartMenu() //open start menu
    {
        manageScene.OpenScene("StartScene", () =>
            {
                //initialize scene after scene finishes loading
                StartMenuScript menuScript = FindObjectOfType<StartMenuScript>();
                menuScript.InitializeScene(this);
            });
    }

}
