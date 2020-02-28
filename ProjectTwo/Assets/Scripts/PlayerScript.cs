using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float health;
    private int score;
    [SerializeField] private float damage;

    //shooting
    private Ray shotRay;
    private Vector3 shootDirection;


    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        score = 0;

        shootDirection = Input.mousePosition;
    }

    void Update() {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                shootDirection = touch.position;
                shotRay = Camera.main.ScreenPointToRay(shootDirection);

                ShootRay(shotRay);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootDirection = Input.mousePosition;
            shotRay = Camera.main.ScreenPointToRay(shootDirection);

            ShootRay(shotRay);
        }
    }

    private void ShootRay(Ray shotRay)
    {
        Debug.DrawRay(shotRay.origin, shotRay.direction, Color.red, 5);
        RaycastHit hit;//Info on the object the ray hit
        if(Physics.Raycast(shotRay.origin, shotRay.direction, out hit))
        {
            hit.transform.gameObject.GetComponent<BaseEnemy>().GetHit(damage);
        }
    }

    // getter of the score so it can be displayed on the ui
    public int GetScore() { return score; }

    // increase the score of the player
    // parameter is the score to add
    public void InceaseScore(int scoreToAdd) { score += scoreToAdd; }

    //// player checking collision with enemies
    //public List<GameObject> CheckCollision(List<GameObject> enemies)
    //{
    //    int length = enemies.Count;
    //    // loop through the enemies to check if there is collision with the player 
    //    for(int i = length-1; i >= 0; i--)
    //    {
    //        // if it is colliding take damage based of the enemies damage
    //        if (true)
    //        {
    //
    //            // destroy the enemy
    //            enemies
    //
    //            // if it did take damage check if the player is now dead
    //            CheckHealth();
    //        }
    //    }
    //
    //    return enemies;
    //}

    void OnCollisionEnter(Collision collision)
    {
        NormalEnemy normal = collision.transform.gameObject.GetComponent<NormalEnemy>();

        normal.GetHit(100);

        health -= normal.Damage();

        CheckHealth();
    }

    // checks if the player is dead 
    private void CheckHealth()
    {
        if(health <= 0.0f)
        {
            Die();
        }
    }

    // called when player has no health and ends game
    private void Die()
    {
        // go to the game over scene or just destroy player & pause scene w/ restart button

    }
}
