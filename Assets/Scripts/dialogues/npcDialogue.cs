using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for the dialogues spoken by the npc
public class npcDialogue
{
    public string id {get;}
    public string dialogue {get;}
    public string dialogueBy {get;}
    public bool isDialogueSelection {get;}

    public string optionResponseID {get;}

    public string sceneID {get;}

    public npcDialogue(string id, string dialogue, string dialogueBy, bool isDialogueSelection, string optionResponseID, string sceneID)
    {
        this.id = id;
        this.dialogue = dialogue;
        this.dialogueBy = dialogueBy;
        this.isDialogueSelection = isDialogueSelection;
        this.optionResponseID = optionResponseID;
        this.sceneID = sceneID;
    }
}
