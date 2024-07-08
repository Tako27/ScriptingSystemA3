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

    public override void Initialize(GameController gameController)
    {
        this.gameController = gameController;
        oriPos = Vector2.zero;
    }

    public void PlayerMovement(Vector2 newPos)
    {
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
