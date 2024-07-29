using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code Done By: Celest Goh Zi Xuan
// ================================
// holds data for game stats
public class SessionDataInfo
{
    public string sessionID;
    public string characterID;
    public string timeSurvived;
    public int totalEnemiesKilled;
    public int level;
    public string mapChosen;
    public Dictionary<string, int> typeOfEnemiesKilled;
}
