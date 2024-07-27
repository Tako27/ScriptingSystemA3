using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script controls the main flow of the game


public class GameController : MonoBehaviour
{
    public GameObject player;
    public InputHandler inputHandler;

    private List<Weapon> weaponInventory;
    private List<item> itemInventory;
    private Character chosenCharacter;
    public DialogueScene dialogueScene;
    public DataManager dataManager;

    public ManageScenes manageScenes;
    public PlayerInventory playerInventory;

    public List<GameObject> mapPrefabs;

    public bool gameActive;
    

    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        dataManager.LoadAllData();
        dialogueScene.OpenDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameActive = true;
        dialogueScene.CloseDialogue();
        
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

        PlayerAttack attackScript = player.GetComponent<PlayerAttack>();
        if (attackScript != null)
        {
            inputHandler.SetAttackReceiver(attackScript);
        }
    }

    // public void OpenPauseMenu()
    // {
    //     manageScenes.OpenScene("Pause", () => 
    //     {
    //             //initialize menu after scene finishes loading
    //             PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
    //             pauseMenu.InitializeMenu(this);

    //         }
    //     );
    // }


}
