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
    public PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadAllData();
        dialogueScene.OpenDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
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
    }


}
