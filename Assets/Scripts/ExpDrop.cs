using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        if(other.CompareTag("Player"))
        {
            
            level.AddExperience(exp);
            Debug.Log("Picked up exp! Amount of exp player has:" + level.GetCurrentExp());
            Destroy(gameObject);
        }
    }
}
