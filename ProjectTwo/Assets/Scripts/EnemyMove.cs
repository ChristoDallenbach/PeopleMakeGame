using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 position = this.transform.position;

        if (position.x < -5)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (position.x > 5)
        {
            direction = new Vector3(-1, 0, 0); ;
        }

        transform.position = transform.position + direction*Time.deltaTime*moveSpeed;
    }
}
