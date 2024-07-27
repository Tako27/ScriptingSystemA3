using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for player dialogues
public class PlayerDialogue
{
    public string triggerID {get;}
    public string id {get;}
    public string dialogue{get;}
    public string dialogueBy{get;}
    public string dialogueType{get;}
    public string typeID{get;}

    public PlayerDialogue(string triggerID, string id, string dialogue, string dialogueBy, string dialogueType, string typeID)
    {
        this.triggerID = triggerID;
        this.id = id;
        this.dialogue = dialogue;
        this.dialogueBy = dialogueBy;
        this.dialogueType = dialogueType;
        this.typeID = typeID;
    }
}
