using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;   

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script handles the dialogues in the game
public class DialogueScene : MonoBehaviour
{
    [SerializeField] GameController gameController;

    [SerializeField] PlayerInventory playerInventory;

    private List<npcDialogue> npcDialogues;
    private List<PlayerDialogue> playerDialogues;

    [SerializeField] TextMeshProUGUI dialogueBy;
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] GameObject dialogueInterface;
    [SerializeField] GameObject gameUI;


    [SerializeField] GameObject nextButton;

    [SerializeField] List<GameObject> responseButtons;
    [SerializeField] List<TextMeshProUGUI> responseButtonText;

    private List<PlayerDialogue> responseOptions;

    public EnemySpawner enemySpawner;

    public CameraBounds cameraBounds;

    [SerializeField] Player player ;

    [SerializeField] private int nextNPCdialogue;

    public bool dialogueOpen;

    public bool toReturnToStartMenu;

    public bool gameStart;

    public string currentSceneID;


    private void Awake()
    {
        gameStart = true;
    }

    void Start()
    {
        cameraBounds  = FindAnyObjectByType<CameraBounds>();
        nextNPCdialogue = 0;
    }


    public void InitializeDialogues()
    {
        responseOptions = new List<PlayerDialogue>();
        npcDialogues = Game.GetNpcDialogueList();
        playerDialogues = Game.GetPlayerDialogueList();
    }

    public void OpenDialogue() //this opens up the dialogue interface
    {
        InitializeDialogues();
        Time.timeScale = 0f; //game is paused during the dialogue
        gameUI.SetActive(false);
        dialogueOpen = true;

        dialogueInterface.SetActive(true);

        if (player == null)
        {
            Debug.LogError("Player is not assigned.");
            return;
        }
        currentSceneID = npcDialogues[nextNPCdialogue].sceneID;
        
        // DS001 - Game Start Dialog
        // DS002 - Game End (player is dead)
        // DS003 - Game End (player is alive)

        if (gameStart)
        {
            dialogueBy.text = npcDialogues[nextNPCdialogue].dialogueBy + ":";
            dialogue.text = npcDialogues[nextNPCdialogue].dialogue; //this is the dialogue made by the npc 
            gameStart = false;
            return;
        }

        if(!player.dead)
        {
            string temp = currentSceneID;

            // skip the current scene's dialogues
            while(temp == currentSceneID)
            {
                currentSceneID = npcDialogues[nextNPCdialogue].sceneID;
                nextNPCdialogue++;
            }
        }
        dialogueBy.text = npcDialogues[nextNPCdialogue].dialogueBy + ":";
        dialogue.text = npcDialogues[nextNPCdialogue].dialogue; //this is the dialogue made by the npc 

    }

    public void CloseDialogue() //this closes the dialogue interface
    {
        Time.timeScale = 1f; //unpause the game
        dialogueInterface.SetActive(false);

        dialogueOpen = false;

        if(currentSceneID == npcDialogues[0].sceneID)
        {
            gameUI.SetActive(true);
            gameController.StartGame();
            enemySpawner.StartSpawning();
        }
        else if(toReturnToStartMenu) //if player has chosen to return to start menu
        {
            gameController.OpenStartMenu();
            gameStart = true;
            toReturnToStartMenu = false;
        }

    }

    public void NextButton() //this handles the dialogue progression
    {
        if(nextNPCdialogue < npcDialogues.Count) //check if dialogue is not at the end of list
        {
            string tempDialogue = null;
            if (currentSceneID == npcDialogues[nextNPCdialogue].sceneID) //if the current dialogue is not the last dialogue of the cutscene
            {
                if(npcDialogues[nextNPCdialogue].isDialogueSelection) //if the current npc dialogue is a trigger event where the player has to choose a response
                {
                    PlayerResponseButton();
                }
                if(npcDialogues[nextNPCdialogue].id == "D1024" || npcDialogues[nextNPCdialogue].id == "D1033")
                {
                    List<Weapon> weaponList = Game.GetWeaponList();
                    Weapon defaultWeapon = weaponList.Find(x => x.id == Game.GetChar().weaponID);
                    tempDialogue = String.Format(npcDialogues[nextNPCdialogue].dialogue, Game.GetTime(), Game.GetmapID(), defaultWeapon.name, Game.GetChar().charName, Game.GetLevel());
                }
                else if(npcDialogues[nextNPCdialogue].id == "D1025" || npcDialogues[nextNPCdialogue].id == "D1034")
                {
                    tempDialogue = String.Format(npcDialogues[nextNPCdialogue].dialogue, Game.GetTotalEnemiesKilled(), Game.GetTotalEnemiesKilled());
                }
                else
                {
                    tempDialogue = npcDialogues[nextNPCdialogue].dialogue;
                }
                dialogueBy.text = npcDialogues[nextNPCdialogue].dialogueBy + ":";
                dialogue.text = tempDialogue; //this is normal npc dialogue --> player does not have to choose response
                nextNPCdialogue++; //increment by 1 to access the next dialogue on the next click
                
                Debug.LogWarning(nextNPCdialogue + " Count:" + npcDialogues.Count);
            }
            else //if current dialogue is the last dialogue of the cutscene, upon pressing on the next button, close the interface
            {
                CloseDialogue();
            }
        }
        else //if dialogue is at end of list
        {
            CloseDialogue();
        }
    }

    public void PlayerResponseButton() //this handles display of player response options
    {
        responseOptions = playerDialogues.FindAll(playerResponse => playerResponse.triggerID == npcDialogues[nextNPCdialogue].id); //this finds all player responses for this dialogue trigger event

        nextButton.SetActive(false);
        for(int i = 0; i<responseOptions.Count; i++) //setting the text for the response options
        {
            responseButtons[i].SetActive(true);
            responseButtonText[i].text = responseOptions[i].dialogue;
        }
    }

    public void SelectResponse(int index) //handles selection of response
    {
        PlayerDialogue response = responseOptions[index];

        npcDialogue nextDialogue = npcDialogues.Find(npc => npc.optionResponseID == response.id); //find the npc dialogue that is connected to the slected response
        //setting next line of dialogue in response to what was selected 
        dialogueBy.text = nextDialogue.dialogueBy + ":";
        dialogue.text = nextDialogue.dialogue;

        if(response.dialogueType == "weaponSelection") //set the default weapon according to the character class chosen in the dialogue
        {
            AddDefultWeaponToInventory(response); //this adds the default weapon to weapon inventory
            SetChosenCharacter(); //this sets the character class according to the weapon selected
            
        }
        else if(response.dialogueType == "mapSelection") //set the map that player will play in according to dialogue choice
        {
            //add set map based on dialogue
            SetMapFromDialogue(response.typeID);
        }
        else if(response.dialogueType == "endGameSelection") //either restart the game or bring the player back to start menu
        {
            //return to start menu after dialogue scene ends
            toReturnToStartMenu = true;
        }

        for(int i = 0; i<responseOptions.Count; i++)
        {
            responseButtons[i].SetActive(false);
        }

        while(nextNPCdialogue < npcDialogues.Count && npcDialogues[nextNPCdialogue].optionResponseID != "null") //iterate to the next dialogue that is not a dialogue that is generated based on player's response after pressing on next button 
        {
            nextNPCdialogue++;
        }

        Debug.LogWarning(nextNPCdialogue + "Count:" + npcDialogues.Count);
        nextButton.SetActive(true);
    }

    public void AddDefultWeaponToInventory(PlayerDialogue playerDialogue) //handles addition of default weapon to weapon inventory based on dialogue choice
    {
        List<Weapon> weapons = Game.GetWeaponList();
        Weapon defaultWeapon = weapons.Find(weapon => weapon.id == playerDialogue.typeID); //find weapon based on dialogue 
        playerInventory.AddWeaponToInventory(defaultWeapon); //add weapon to inventory

    }

    public void SetChosenCharacter() //set character class based on dialogue choice
    {
        List<Weapon> defaultWeapon = playerInventory.GetWeaponInventory();
        List<Character> charList = Game.GetCharList();

        foreach(Weapon weapon in defaultWeapon)
        {
            Character chosenCharacter = charList.Find(character => character.weaponID == weapon.id); //find the character class that is selected based on dialogue 
            Game.SetChar(chosenCharacter); //set character class 
        }

    }

    public void SetMapFromDialogue(string mapID) //set map based on dialogue choice
    {
        List<string> MapIDs = Game.GetMapIDs();
        foreach(string map in MapIDs)
        {
            if (map == mapID) //find the matching map id that is selected based on dialogue
            {
                Game.SetMapID(map); //set the map
                enemySpawner.GetWavesByMap(); //initialize enemySpawner based on selected map
                foreach(GameObject mapPrefab in gameController.mapPrefabs)
                {
                    if (mapPrefab.name == mapID)
                    {
                        GameObject mapInstance = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        enemySpawner.GetSpawnPointsByMap(mapID);
                        cameraBounds.AssignCameraBounds(mapInstance); //assign camera boundary accoring to map
                    }
                }
            }
        }
    }
}
