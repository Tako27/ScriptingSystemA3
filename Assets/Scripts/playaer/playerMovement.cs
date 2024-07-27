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

    private float speedMultiplier;
    
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
        speedMultiplier = player.speed; //this is the speed multiplier taken from the selected character stats, changes according to specific upgrades


        movementSpeed = 2f * speedMultiplier; //the actual movement speed of the player is the speed multiplier of selected character class multiplied by base movement speed

        oriPos = newPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        oriPos.Normalize();

        Vector2 movePos = rb.position + oriPos*movementSpeed*Time.fixedDeltaTime; //player moven=ment calculation
        rb.MovePosition(movePos); //move player

    }

    
}
