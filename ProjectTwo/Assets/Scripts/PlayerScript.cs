using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float health;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        score = 0;
    }

    // getter of the score so it can be displayed on the ui
    public int GetScore() { return score; }

    // increase the score of the player
    // parameter is the score to add
    public void InceaseScore(int scoreToAdd) { score += scoreToAdd; }

    // checks if the player is dead 
    public void CheckHealth()
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
