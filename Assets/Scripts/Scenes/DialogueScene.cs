using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class DialogueScene : MonoBehaviour
{
    [SerializeField] GameController gameController;

    [SerializeField] PlayerInventory playerInventory;

    private List<npcDialogue> npcDialogues;
    private List<PlayerDialogue> playerDialogues;

    [SerializeField] TextMeshProUGUI dialogueBy;
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] GameObject dialogueInterface;
    [SerializeField] GameObject expBar;

    [SerializeField] GameObject nextButton;

    [SerializeField] List<GameObject> responseButtons;
    [SerializeField] List<TextMeshProUGUI> responseButtonText;

    private List<PlayerDialogue> responseOptions;

    private int nextNPCdialogue;

    public bool dialogueOpen;

    public void InitializeDialogues()
    {
        responseOptions = new List<PlayerDialogue>();
        npcDialogues = Game.GetNpcDialogueList();
        playerDialogues = Game.GetPlayerDialogueList();
    }

    public void OpenDialogue()
    {
        nextNPCdialogue = 1; //start at 1 since we want to go to the next dialogue made by the npc after clicking on next button
        dialogueOpen = true;
        InitializeDialogues();
        Time.timeScale = 0f;
        dialogueInterface.SetActive(true);

        dialogueBy.text = npcDialogues[0].dialogueBy + ":";
        dialogue.text = npcDialogues[0].dialogue; //this is the first dialogue made by the npc
 
    }

    public void CloseDialogue()
    {
        Time.timeScale = 1f;
        dialogueInterface.SetActive(false);
        expBar.SetActive(true);
        dialogueOpen = false;

    }

    public void NextButton()
    {
        
        if(nextNPCdialogue<npcDialogues.Count)
        {
            if(npcDialogues[nextNPCdialogue].isDialogueSelection)
            {
                PlayerResponseButton();
            }
            dialogue.text = npcDialogues[nextNPCdialogue].dialogue;
            nextNPCdialogue++; //increment by 1 to access the next dialogue on the next click
        }
        else
        {
            gameController.StartGame();
        }
        
    }

    public void PlayerResponseButton()
    {
        responseOptions = playerDialogues.FindAll(playerResponse => playerResponse.triggerID == npcDialogues[nextNPCdialogue].id); //this finds all player responses for this dialogue trigger event

        nextButton.SetActive(false);
        for(int i = 0; i<responseOptions.Count; i++)
        {
            responseButtons[i].SetActive(true);
            responseButtonText[i].text = responseOptions[i].dialogue;
        }
    }

    public void SelectResponse(int index)
    {
        PlayerDialogue response = responseOptions[index];

        npcDialogue nextDialogue = npcDialogues.Find(npc => npc.optionResponseID == response.id);

        dialogueBy.text = nextDialogue.dialogueBy + ":";
        dialogue.text = nextDialogue.dialogue;

        if(response.dialogueType == "weaponSelection")
        {
            AddDefultWeaponToInventory(response);
            SetChosenCharacter();
            
        }
        else if(response.dialogueType == "mapSelection")
        {
            //add set map based on dialogue

        }

        for(int i = 0; i<responseOptions.Count; i++)
        {
            responseButtons[i].SetActive(false);
        }

        while(nextNPCdialogue < npcDialogues.Count && npcDialogues[nextNPCdialogue].optionResponseID != "null")
        {
            nextNPCdialogue++;
        }
        nextButton.SetActive(true);
    }

    public void AddDefultWeaponToInventory(PlayerDialogue playerDialogue)
    {
        List<Weapon> weapons = Game.GetWeaponList();
        Weapon defaultWeapon = weapons.Find(weapon => weapon.id == playerDialogue.typeID);
        playerInventory.AddWeaponToInventory(defaultWeapon);

    }

    public void SetChosenCharacter()
    {
        List<Weapon> defaultWeapon = playerInventory.GetWeaponInventory();
        List<Character> charList = Game.GetCharList();

        foreach(Weapon weapon in defaultWeapon)
        {
            Character chosenCharacter = charList.Find(character => character.weaponID == weapon.id);
            Game.SetChar(chosenCharacter);

            Debug.Log(Game.GetChar().charName);
            Debug.Log(weapon.name);
        }

    }
    
}
