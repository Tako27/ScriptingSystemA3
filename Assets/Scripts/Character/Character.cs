using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string id {get;}
    public string charName {get; private set;}
    public float health {get; private set;}
    public float moveSpd {get; private set;}
    public float atkSpd {get; private set;}
    public float atkMultiplier {get; private set;}
    public string weaponID {get; private set;}

    public Character(string id, string charName, float health,float moveSpd,float atkSpd,float atkMultiplier,string weaponID)
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
