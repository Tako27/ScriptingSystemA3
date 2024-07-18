using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private InputReceiver inputReceiver;

    public void SetInputReceiver(InputReceiver receiver)
    {
        inputReceiver = receiver;
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
        
    }
}
