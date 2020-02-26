using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject closestEnemy;
    private NormalEnemy closestScript; //Replace Transform with enemy script once it's been created
    private GameObject player;
    private PlayerScript playerScript; //Replace Transform with playerscript once it's been created;
    private List<GameObject> enemyList;

    private float score;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerScript = player.GetComponent<PlayerScript>();
        enemyList = new List<GameObject>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPause())//See if the game's paused
        {
            //if the game is not paused?
            GenerateEnemies();//Code to check if new enemies need to be generated, or if the closest enemy needs to be changed
        }
    }

    bool CheckPause()
    {
        return false;
    }

    void GenerateEnemies()
    {

    }
    private void PauseToggle(bool pause)
    {
        //if we're not paused
        //set to pause
        //stop everything
        // pull up pause menu
        
        //if we are paused
        //we are now unpausing
        //resume everything
        //close pause menu
    }
}
