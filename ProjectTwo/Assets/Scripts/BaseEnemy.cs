using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float health, damage, moveSpeed;
    [SerializeField] protected int scoreValue;
    protected Vector3 direction;
    protected bool dying;//Allows us to implement a death animation if we want to
    protected bool collisionDeath;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        Vector3 destination = Random.insideUnitSphere.normalized;
        Vector3 pos = new Vector3(destination.x, Mathf.Abs(destination.y/5), Mathf.Abs(destination.z)).normalized *17.5f;
        transform.position = Camera.main.transform.position + pos;//sets position to a random spot near the camera
        transform.LookAt(Camera.main.transform);
        dying = false;
        collisionDeath = false;
        Vector3 distance = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    abstract protected void Move();

    protected void TakeDamage(float damage){
        health -= damage;
        if (health <= 0)
        {
            dying = true;
        }
    }

    public bool isDying()
    {
        return dying;
    }

    public float Damage()
    {
        return damage;
    }
    
    public int ScoreValue()
    {
        return scoreValue;
    }
    
    /// <summary>
    /// Checks collision
    /// </summary>
    /// <param name="collision">The colliding object, plus other info</param>
    abstract public void GetHit(float damage, TapType type);

    public void DestroyOnColl()
    {
        collisionDeath = true;
    }

    public bool isColliding()
    {
        return collisionDeath;
    }

}
