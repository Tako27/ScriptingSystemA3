using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    public string id {get;}
    public string name {get;}

    public float damage {get;}

    public string weaponType {get;}
    public string basicDesc {get;}

    public bool isGeneric {get;}

    public Weapon(string id, string name, float damage,string weaponType, string basicDesc, bool isGeneric)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.weaponType = weaponType;
        this.basicDesc = basicDesc;
        this.isGeneric = isGeneric;
    }

    public string GetID()
    {
        return id;
    }
}
