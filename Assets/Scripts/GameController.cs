using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script controls the main flow of the game

public class GameController : MonoBehaviour
{
    public GameObject player;
    public InputHandler inputHandler;

    private List<Weapon> weapons;
    private List<item> items;
    private List<Character> characters;
    private List<string> MapIDs;

    public DataManager dataManager;
    private bool gameActive;

    // shift this to where you want select the map
    private string selectedMap = "M01";
    // remember drag and drop assign enemySpawner
    public EnemySpawner enemySpawner;
    // ===

    // Start is called before the first frame update
    void Start()
    {
        dataManager.LoadAllData();
        StartGame(); //change later
        weapons = Game.GetWeaponList();
        items = Game.GetItemList();
        characters = Game.GetCharList();

        // get list of maps
        MapIDs = Game.GetMapIDs();
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

        // === to be shifted elsewhere: start

        // when player selected a map from the mapIDs list
        Game.SetMapID(selectedMap);
        enemySpawner.GetWavesByMap();
        enemySpawner.StartSpawning();
        // === to be shifted elsewhere: end
    }

    //for testingg

}
