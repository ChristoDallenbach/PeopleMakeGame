using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float health, damage, moveSpeed;
    public Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        
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

        }
    }

    /// <summary>
    /// Checks collision
    /// </summary>
    /// <param name="collision">The colliding object, plus other info</param>
    abstract protected void OnCollisionEnter(Collision collision);
}
