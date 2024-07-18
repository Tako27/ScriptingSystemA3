using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item
{
    public string id {get;}
    public string name {get;}

    public string effectType {get;}

    public string basicDesc {get; set;}

    public int initiallevel = 1;

    public item(string id, string name, string effectType, string basicDesc)
    {
        this.id = id;
        this.name = name;
        this.effectType = effectType;
        this.basicDesc = basicDesc;
    }

    public string GetID()
    {
        return id;
    }

    public string GetDescription()
    {
        return basicDesc;
    }
}