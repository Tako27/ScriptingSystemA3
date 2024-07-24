using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the reference class for items
// It takes the data pulled from the csv in datamanager script and then assigns the data to the various fields
// It is then used in item script
public class refItem
{
    public string id;
    public string name;
    public string effectType;

    public string basicDesc;

    public float itemValue;

    public string effectTime;

    public string effectCooldown;

    public string imageFilePath;
}
