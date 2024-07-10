using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item
{
    public string id;
    public string name;

    public string effectType;

    public item(string id, string name, string effectType)
    {
        this.id = id;
        this.name = name;
        this.effectType = effectType;
    }

    public string GetID()
    {
        return id;
    }
}