using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 temp = Random.insideUnitCircle.normalized * 10;
        transform.position = Camera.main.transform.position + new Vector3(temp.x, 0, Mathf.Abs(temp.y));//sets position to a random spot near the camera
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
    /// Checks collision
    /// </summary>
    /// <param name="collision">The colliding object, plus other info</param>
    public override void GetHit(float damage)
    {
        base.TakeDamage(this.damage);
    }
}
