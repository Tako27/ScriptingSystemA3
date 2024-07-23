using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the reference class for character classes
// It takes the data pulled from the csv in datamanager script and then assigns the data to the various fields
// It is then used in Character script
public class refChar
{
    public string id;
    public string charName;
    public float health;
    public float moveSpd;
    public float atkSpd;
    public float atkMultiplier;
    public string weaponID;
}
