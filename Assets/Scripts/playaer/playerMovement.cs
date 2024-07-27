using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the player movement

public class playerMovement : PlayerScript, InputReceiver
{
    private GameController gameController;
    Rigidbody2D rb;

    public float movementSpeed;
    private Vector2 oriPos;
    
    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    public override void Initialize(GameController gameController)
    {
        this.gameController = gameController;
        oriPos = Vector2.zero;
    }

    public void PlayerMovement(Vector2 newPos)
    {
        movementSpeed = 2f*player.speedMultiplier; //this is the player speed is the speed multiplier for movement speed, 2f is the base speed of all characters

        oriPos = newPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        oriPos.Normalize();

        Vector2 movePos = rb.position + oriPos*movementSpeed*Time.fixedDeltaTime; //player moven=ment calculation
        rb.MovePosition(movePos); //move player

    }

    
}
