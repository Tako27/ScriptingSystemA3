using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement : PlayerScript, InputReceiver
{
    private GameController gameController;
    Rigidbody2D rb;
    
    public float moveSpd = 10f;
    private Vector2 oriPos;

    private float speedMultiplier;

    public override void Initialize(GameController gameController)
    {
        this.gameController = gameController;
        oriPos = Vector2.zero;
    }

    public void PlayerMovement(Vector2 newPos)
    {
        List<Character> character = Game.GetCharList();

        foreach (Character chara in character)
        {
            speedMultiplier = chara.moveSpd; //set movement speed multiplier for the player according to character
        }

        oriPos = newPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        oriPos.Normalize();

        Vector2 movePos = rb.position + oriPos*moveSpd*Time.fixedDeltaTime;
        rb.MovePosition(movePos);

        if(oriPos.magnitude!=0)
        {
            rb.transform.up = oriPos;
        }
    }

    
}
