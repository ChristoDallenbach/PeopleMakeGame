using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    public Vector3 direction;
    private Vector3 cameraMin, cameraMax;


    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(1,0,0);
        cameraMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
        cameraMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 position = this.transform.position;

        if (position.x < cameraMin.x)
        {
            direction = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        }
        else if (position.x > cameraMax.x)
        {
            direction = new Vector3(Random.Range(-1.0f, 0.0f), Random.Range(-1.0f, 1.0f), 0);
        }
        if (position.y < cameraMin.y)
        {
            direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        }
        else if (position.y > cameraMax.y)
        {
            direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0.0f), 0);
        }
        direction = direction.normalized;

        transform.position = transform.position + direction*Time.deltaTime*moveSpeed;
    }
}
