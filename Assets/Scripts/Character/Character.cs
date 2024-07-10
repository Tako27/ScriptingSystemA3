using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string id {get;}
    public string charName {get;}
    public float health {get;}
    public float moveSpd {get;}
    public float atkSpd {get;}
    public float atkMultiplier {get;}
    public string weaponID {get;}

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
