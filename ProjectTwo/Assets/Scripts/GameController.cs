using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject closestEnemy;
    private NormalEnemy closestScript; //Replace Transform with enemy script once it's been created
    private GameObject player;
    private PlayerScript playerScript; //Replace Transform with playerscript once it's been created;
    private List<GameObject> enemyList;

    public static int score;
    private float lastEnemy;
    [Tooltip("Float between 0 and 1")]
    [SerializeField] private float enemySpawnChance;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private GameObject[] specialEnemyList;
    [SerializeField] private GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        enemyList = new List<GameObject>();
        score = 0;
        GameObject.Find("scoreText").GetComponent<Text>().text = "Score: " + score;
        lastEnemy = 3.0f;

        StartCoroutine(IncreaseSpawnRate());
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckPause())//See if the game's paused
        {
            //if the game is not paused?
            GenerateEnemies();//Code to check if new enemies need to be generated, or if the closest enemy needs to be changed
            for (int i = 0; i < enemyList.Count; i++){
                if (enemyList[i].GetComponent<BaseEnemy>().isColliding())
                {
                    Debug.Log(score);

                    Destroy(enemyList[i].gameObject, 0.1f);
                    enemyList.RemoveAt(i);
                }
                else if (enemyList[i].GetComponent<BaseEnemy>().isDying())
                {
                    InceaseScore(enemyList[i].GetComponent<BaseEnemy>().ScoreValue());
                    Debug.Log(score);

                    GameObject.Find("scoreText").GetComponent<Text>().text = "Score: " + score;

                    enemyList.RemoveAt(i);
                }
            }

            // calling find closest enemy at the end of each frame
            FindClosestEnemy();
        }
        Debug.DrawRay(indicator.transform.position, indicator.transform.up, Color.red);
    }

    bool CheckPause()
    {
        return false;
    }

    void GenerateEnemies()
    {
        if (lastEnemy > spawnRate){//Checks if the time between enemy spawns is larger than the rate enemies should be spawning
            if (enemySpawnChance < Random.value) //Add a little randomness to the spawning, to make it feel more natural
            {
                if (Random.value < 0.7f){
                    enemyList.Add(GameObject.Instantiate(baseEnemy, new Vector3(indicator.transform.up.x, indicator.transform.up.y, indicator.transform.up.z) * 100, Quaternion.Euler(0, 0, 0)));//instantiates the object far away.  Will be moved later
                    lastEnemy = 0.0f;
                }
                else{
                    int enemy = Random.Range(0, specialEnemyList.Length);
                    enemyList.Add(GameObject.Instantiate(specialEnemyList[enemy], new Vector3(indicator.transform.up.x, indicator.transform.up.y, indicator.transform.up.z)*100, Quaternion.Euler(0, 0, 0)));//instantiates the object far away.  Will be moved later
                    SpawnEnemy enemySpawn;
                    if (enemyList[enemyList.Count - 1].TryGetComponent<SpawnEnemy>(out enemySpawn))//checks if selected enemy is a spawner
                    {
                        //if so, give them a reference to the enemy list so they can add their enemies into the list
                        enemySpawn.enemyList = enemyList;
                    }
                    lastEnemy = 0.0f;
                }
            }
        }
        lastEnemy += Time.deltaTime;
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

    // method for finding the closest enemy
    public void FindClosestEnemy()
    {
        if (enemyList.Count > 1)//Check to make sure that their are more than 1 enemy in the scene
        {
            // getting the smallest magnitude between player and object
            float minDistance = (enemyList[0].transform.position - player.transform.position).magnitude;
            int smallestIndex = 0;

            // looping through to find the smallest distance
            for (int i = 1; i < enemyList.Count; i++)
            {
                if ((enemyList[i].transform.position - player.transform.position).magnitude < minDistance)
                {
                    minDistance = (enemyList[i].transform.position - player.transform.position).magnitude;
                    smallestIndex = i;
                }
            }

            // setting which enemy is closest to the player
            closestEnemy = enemyList[smallestIndex];
            indicator.transform.LookAt(closestEnemy.transform.position);
            indicator.transform.Rotate(new Vector3(90,0,0));
        }
        else if(enemyList.Count == 1){//If there's exactly 1 enemy, it's the closest by default
            closestEnemy = enemyList[0];
            indicator.transform.LookAt(closestEnemy.transform.position);
            indicator.transform.Rotate(new Vector3(90, 0, 0));
        }
    }

    private IEnumerator IncreaseSpawnRate()
    {
        //Every second, enemies start spawning 1/10th of a second quicker
        while (spawnRate > 0.75f)
        {
            yield return new WaitForSeconds(1);
            spawnRate -= 0.025f;
        }
    }

    /// <summary>
    /// increase the score of the player
    /// </summary>
    /// <param name="scoreToAdd">score to add</param>
    public void InceaseScore(int scoreToAdd) { score += scoreToAdd; }

}

