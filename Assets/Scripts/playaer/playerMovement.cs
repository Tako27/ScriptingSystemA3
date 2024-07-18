using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement : PlayerScript, InputReceiver
{
    private GameController gameController;
    Rigidbody2D rb;

    public float movementSpeed;
    private Vector2 oriPos;

    private float speedMultiplier;

    public override void Initialize(GameController gameController)
    {
        this.gameController = gameController;
        oriPos = Vector2.zero;
    }

    public void PlayerMovement(Vector2 newPos)
    {
        speedMultiplier = Game.GetChar().moveSpd;

        movementSpeed = 4f * speedMultiplier;

        oriPos = newPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        oriPos.Normalize();

        Vector2 movePos = rb.position + oriPos*movementSpeed*Time.fixedDeltaTime;
        rb.MovePosition(movePos);

        if(oriPos.magnitude!=0)
        {
            rb.transform.up = oriPos;
        }
    }

    
}
