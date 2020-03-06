using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float health, damage, moveSpeed;
    [SerializeField] protected int scoreValue;
    protected Vector3 direction;
    protected bool dying;//Allows us to implement a death animation if we want to

    // Start is called before the first frame update
    virtual protected void Start()
    {
        Vector3 temp = Random.insideUnitCircle.normalized * 10;
        transform.position = Camera.main.transform.position + new Vector3(temp.x, 0, Mathf.Abs(temp.y));//sets position to a random spot near the camera
        dying = false;   
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
}
