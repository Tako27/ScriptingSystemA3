using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is for handling tracker for total time elapsed in the game
public class TimeTracker : MonoBehaviour
{
    public float timeElapsed;

    [SerializeField] GameController gameController;
    [SerializeField] private TextMeshProUGUI timeText;

    public string endText;
    
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
            timeElapsed+= Time.deltaTime; //total time elapsed in seconds
            timeText.text = "Time Elapsed:" + "\n" + Timer(timeElapsed); //set timer text
        }
        
    }

    private string Timer(float time) //handle timer text
    {
        int minutes = Mathf.FloorToInt(time/60); //get minutes passed
        int seconds = Mathf.FloorToInt(time%60); //get seconds passed
        if(minutes<10)
        {
            if(seconds<10)
            {
                endText = "0"+ minutes + ":" + "0" + seconds;
            }
            else
            {
                endText = "0"+ minutes + ":" + seconds;
            }
            
        }
        else
        {
            if(seconds<10)
            {
                endText = minutes + ":" + "0" + seconds;
            }
            else
            {
                endText = minutes + ":" + seconds;
            }
            
        }


        return endText;
    }

}
