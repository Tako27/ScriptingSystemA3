using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public InputHandler inputHandler;

    private List<Weapon> weapons;
    private List<item> items;
    private List<Character> characters;


    public DataManager dataManager;
    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadAllData();
        StartGame(); //change later
        weapons = Game.GetWeaponList();
        items = Game.GetItemList();
        characters = Game.GetCharList();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        player.transform.position = player.transform.position;
        foreach(PlayerScript playerScript in player.GetComponents<PlayerScript>())
        {
            playerScript.Initialize(this);
        }

        inputHandler.SetInputReceiver(player.GetComponent<playerMovement>());
    }

    //for testingg
    
}
