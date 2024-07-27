using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script is for handling player key inputs

public class InputHandler : MonoBehaviour
{
    private InputReceiver inputReceiver;
    private IAttackReceiver attackReceiver;

    public void SetInputReceiver(InputReceiver receiver)
    {
        inputReceiver = receiver;
    }

    public void SetAttackReceiver(IAttackReceiver receiver)
    {
        attackReceiver = receiver;
    }

    void FixedUpdate()
    {
        if(inputReceiver == null)
        {
            return;
        }

        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        Vector2 movePos = new Vector2(hori, verti);
        inputReceiver.PlayerMovement(movePos);
    }

    // Update is called once per frame
    void Update()
    {
        // if attack receiver not null, and left click detected
        if (attackReceiver != null && Input.GetButtonDown("Fire1")) 
        {
            // getting position of the mouse cursor in screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // convert the mouse position to world coordinates
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // ignore z axis
            worldMousePos.z = 0;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // get player position
                Vector3 playerPos = player.transform.position;

                // calculate the direction from the player to the mouse cursor
                Vector2 attackDirection = (Vector2)(worldMousePos - playerPos);

                // normalize direction vector to ensure it has magnitude of 1
                attackDirection.Normalize();

                // call player attack method on attackReceiver
                attackReceiver.AttackAction(attackDirection);
            }
        }
        
    }
}
