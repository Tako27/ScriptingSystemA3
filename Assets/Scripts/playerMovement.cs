using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    float hori;
    float vert;

    Vector2 oriPos;
    [SerializeField] private float moveSpeed = 10f;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oriPos = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hori = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(hori, vert);
        MovePlayer(movement);
    }

   void MovePlayer(Vector2 newPos)
   {
        oriPos = newPos;
        oriPos.Normalize();
        Vector2 movePos = rb.position + oriPos*moveSpeed*Time.fixedDeltaTime;
        rb.MovePosition(movePos);
        if(oriPos.magnitude!=0)
        {
            rb.transform.up = oriPos;
        }
   }



}
