using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public InputHandler inputHandler;
    // Start is called before the first frame update
    void Start()
    {
        StartGame(); //change later
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        player.transform.position = Vector2.zero;
        foreach(PlayerScript playerScript in player.GetComponents<PlayerScript>())
        {
            playerScript.Initialize(this);
        }

        inputHandler.SetInputReceiver(player.GetComponent<playerMovement>());
    }
}
