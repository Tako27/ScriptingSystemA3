using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script is for handling player behaviour

// interface for handling player movement
public interface InputReceiver
{
    void PlayerMovement(Vector2 newPos); //player movement
    
}

//interface for handling player attacking
public interface IAttackReceiver
{
    void AttackAction(Vector2 attackDirection);
}
