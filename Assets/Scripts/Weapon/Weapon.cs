using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for weapons
public class Weapon 
{
    public string id {get;}
    public string name {get;}

    public float damage {get;}

    public string weaponType {get;}
    public string basicDesc {get; set;}

    public bool isGeneric {get;}

    public int initialLevel = 1;

    public int projectileCount;

    public float dmgMultiplier;

    public float fireRate;

    public float weaponRangeMultiplier = 1;
    public float RegenProbability = 0;
    public float RecoveryMultiplier = 0;

    public string imageFilePath {get; set;}

    public Weapon(string id, string name, float damage,string weaponType, string basicDesc, bool isGeneric, string imageFilePath)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.weaponType = weaponType;
        this.basicDesc = basicDesc;
        this.isGeneric = isGeneric;
        this.imageFilePath = imageFilePath;
    }

}
