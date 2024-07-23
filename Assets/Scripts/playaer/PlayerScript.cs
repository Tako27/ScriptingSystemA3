using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is inherited by all player scripts that receives input from player
public abstract class PlayerScript : MonoBehaviour
{
    public abstract void Initialize(GameController gameController);
}
