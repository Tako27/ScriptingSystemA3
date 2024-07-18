using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue
{
    public string id {get;}
    public string dialogue {get;}
    public string dialogueBy {get;}
       public bool isDialogueSelection {get;}

    public string optionResponseID {get;}

    public npcDialogue(string id, string dialogue, string dialogueBy, bool isDialogueSelection, string optionResponseID)
    {
        this.id = id;
        this.dialogue = dialogue;
        this.dialogueBy = dialogueBy;
        this.isDialogueSelection = isDialogueSelection;
        this.optionResponseID = optionResponseID;
    }
}
