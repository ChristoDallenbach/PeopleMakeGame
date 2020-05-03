using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    //A simple boss, has 3 stages.  Each stage the boss is faster and smaller, but does less damage and has less health.
    private int stage;
    private float startingHealth;
    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
        startingHealth = base.health;
        Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            Destroy(gameObject, 0.1f);//Destroys the game object after a short delay
        }
        else
        {
            Move();
        }
    }

    /// <summary>
    /// Helper method to re-do all starting logic on stage change, without resetting stage or startingHealth
    /// </summary>
    private void Restart()
    {
        base.Start();
    }

    protected override void Move()
    {
        direction = Camera.main.transform.position - transform.position;
        direction = direction.normalized;

        transform.position = transform.position + direction * Time.deltaTime * moveSpeed;
    }

    /// <summary>
    /// Checks collision.  This enemy doesn't care what kind of tap hits it, so type is ignored
    /// </summary>
    /// <param name="collision">The colliding object, plus other info</param>
    public override void GetHit(float damage, TapType type)
    {
        TakeDamage(damage);
    }

    new protected void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (stage == 1)
            {
                stage++;
                moveSpeed *= 2;
                health = startingHealth/2;
                Restart();
            }
            else if (stage == 2)
            {
                stage++;
                moveSpeed *= 4;
                health = startingHealth/4;
                Restart();
            }
            else if (stage == 3)
            {
                dying = true;
            }
        }
    }

    public void AddBonusHealth(float bonus)
    {
        health += bonus;
    }
}
