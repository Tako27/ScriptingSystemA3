using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the reference class for npc dialogues
// It takes the data pulled from the csv in datamanager script and then assigns the data to the various fields
// It is then used in npcdialogue script
public class refNpcDialogue
{
    public string id;
    public string dialogue;
    public string dialogueBy;
    public bool isDialogueSelection;
    public string optionResponseID;

    public string sceneID;
    
}
