using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy
{
    [SerializeField] private float health, damage, moveSpeed;
    public Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
    protected override void OnCollisionEnter(Collision collision)
    {
        base.TakeDamage(damage);
    }
}
