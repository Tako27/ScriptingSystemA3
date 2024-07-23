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
    public string charName {get; private set;}
    public float health {get; private set;}
    public float moveSpd {get; private set;}
    public float atkSpd {get; private set;}
    public float atkMultiplier {get; private set;}
    public string weaponID {get; private set;}

    public Character(string id, string charName, float health,float moveSpd,float atkSpd,float atkMultiplier,string weaponID) //constructor for character class
    {
        this.id = id;
        this.charName = charName;
        this.health = health;
        this.moveSpd = moveSpd;
        this.atkSpd = atkSpd;
        this.atkMultiplier = atkMultiplier;
        this.weaponID = weaponID;
        
    }
}
