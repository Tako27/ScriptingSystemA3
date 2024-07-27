using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for items
public class item
{
    public string id {get;}
    public string name {get;}

    public string effectType {get;}

    public string basicDesc {get; set;}

    public float itemValue {get; set;}

    public float effectTime {get; set;}

    public float effectCooldown {get; set;}

    public string imageFilePath {get; set;}

    public int initiallevel = 1; //set all item levels to level 1 by default

    public item(string id, string name, string effectType, string basicDesc, float itemValue, float effectTime, float effectCooldown, string imageFilePath)
    {
        this.id = id;
        this.name = name;
        this.effectType = effectType;
        this.basicDesc = basicDesc;
        this.itemValue = itemValue;
        this.effectTime = effectTime;
        this.effectCooldown = effectCooldown;
        this.imageFilePath = imageFilePath;
    }
}