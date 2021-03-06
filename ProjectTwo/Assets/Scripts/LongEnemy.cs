﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemy : BaseEnemy
{

    public bool isClone = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isClone)
        {
            base.Start();
        }
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
    public override void GetHit(float damage, TapType type)
    {
        if (type == TapType.shortTap)
        {
            Vector3 temp = Random.insideUnitCircle.normalized;
            Vector3 position = transform.position + new Vector3(temp.x*3, temp.y, 0);//sets position to a random spot near the original object
            GameObject tempObject = GameObject.Instantiate(gameObject, position, Quaternion.identity);
            tempObject.GetComponent<LongEnemy>().isClone = true;
            tempObject.transform.LookAt(Camera.main.transform);
        }
        else if (type == TapType.longTap)
            base.TakeDamage(this.damage);

    }
}
