using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for available character classes
public class Character
{
    //get necessary values from reference class
    public string id {get;}
    public string charName {get; set;}
    public float health {get; set;}
    public float moveSpd {get; set;}
    public float atkSpd {get; set;}
    public float atkMultiplier {get; set;}
    public string weaponID {get; set;}

    public string spriteImage {get; set;}

    public Character(string id, string charName, float health,float moveSpd,float atkSpd,float atkMultiplier,string weaponID, string spriteImage) //constructor for character class
    {
        this.id = id;
        this.charName = charName;
        this.health = health;
        this.moveSpd = moveSpd;
        this.atkSpd = atkSpd;
        this.atkMultiplier = atkMultiplier;
        this.weaponID = weaponID;
        this.spriteImage = spriteImage;
    }
}
