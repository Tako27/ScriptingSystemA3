using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float moveSpd = 5.0f;

    Rigidbody2D rb;

    Transform player;

    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        moveDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = (player.position - transform.position).normalized;
        if(moveDir.magnitude!=0)
        {
            rb.transform.up = moveDir;
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (moveDir.x*moveSpd, moveDir.y*moveSpd);
    }
}
