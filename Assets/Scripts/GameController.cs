using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player.transform.position = Vector2.zero;
        foreach(PlayerScript playerScript in player.GetComponents<PlayerScript>())
        {
            playerScript.Initialize(this);
        }

        inputHandler.SetInputReceiver(player.GetComponent<playerMovement>());
    }

    
}
