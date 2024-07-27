using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the reference class for weapons
// It takes the data pulled from the csv in datamanager script and then assigns the data to the various fields
// It is then used in weapon script
public class refWeapon
{
    public string id;
    public string name;

    public float damage;

    public string weaponType;
    public string basicDesc;

    public bool isGeneric;

    public string imageFilePath;
    
}

