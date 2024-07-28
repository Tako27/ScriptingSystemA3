using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is for handling tracker for total time elapsed in the game
public class TimeTracker : MonoBehaviour
{
    private float timeElapsed;

    [SerializeField] GameController gameController;
    [SerializeField] private TextMeshProUGUI timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.gameActive)
        {
            timeElapsed+= Time.unscaledDeltaTime; //total time elapsed in seconds
            timeText.text = "Time Elapsed:" + "\n" + Timer(timeElapsed); //set timer text
        }
        
    }

    private string Timer(float time) //handle timer text
    {
        string text = null;
        int minutes = Mathf.FloorToInt(time/60); //get minutes passed
        int seconds = Mathf.FloorToInt(time%60); //get seconds passed
        if(minutes<10)
        {
            if(seconds<10)
            {
                text = "0"+ minutes + ":" + "0" + seconds;
            }
            else
            {
                text = "0"+ minutes + ":" + seconds;
            }
            
        }
        else
        {
            if(seconds<10)
            {
                text = minutes + ":" + "0" + seconds;
            }
            else
            {
                text = minutes + ":" + seconds;
            }
            
        }


        return text;
    }
}
