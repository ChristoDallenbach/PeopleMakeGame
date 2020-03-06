using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {

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
        base.TakeDamage(this.damage);
    }
}
