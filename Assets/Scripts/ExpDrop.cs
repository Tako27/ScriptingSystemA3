using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is for handling collison with exp drops from enemies

public class ExpDrop : MonoBehaviour
{
    GameObject player;
    public int exp = 400;
    Level level;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        level = player.GetComponent<Level>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) //upon collision with an exp drop, gain experience and destroy the current exp drop
        {
            
            level.AddExperience(exp);
            Destroy(gameObject);
        }
    }
}
