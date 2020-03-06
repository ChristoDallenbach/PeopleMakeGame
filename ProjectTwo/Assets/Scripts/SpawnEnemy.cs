using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : BaseEnemy
{
    [SerializeField] private GameObject spawn;
    [SerializeField] private float spawnRate;
    private float spawnTime;
    private float spawnAngle;
    public List<GameObject> enemyList;//The list of all enemies.  This is passed into the script on creation by the game controller


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        spawnAngle = 0;
        spawnTime = 0;
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
            Attack();
        }
    }

    protected override void Move()
    {
        direction = Camera.main.transform.position - transform.position;
        direction = direction.normalized;
    }

    protected void Attack()
    {
        if (spawnTime > spawnRate)
        {
            float x = Mathf.Cos(spawnAngle * Mathf.Deg2Rad);
            float y = Mathf.Sin(spawnAngle * Mathf.Deg2Rad);
            Vector3 tempVec = transform.position;
          
            Vector3 dir = direction.normalized;
            GameObject temp = GameObject.Instantiate(spawn, new Vector3(x,y,dir.z) + transform.position, Quaternion.identity);
            temp.transform.LookAt(Camera.main.transform);
            spawnAngle += 20;
            spawnTime = 0;
            enemyList.Add(temp);
        }
        else
            spawnTime += Time.deltaTime;
    }

    /// <summary>
    /// Checks collision
    /// </summary>
    /// <param name="collision">The colliding object, plus other info</param>
    public override void GetHit(float damage, TapType type)
    {
        base.TakeDamage(this.damage);
    }
}
