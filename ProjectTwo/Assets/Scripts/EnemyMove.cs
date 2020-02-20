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
        direction = Camera.main.transform.position - transform.position;
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

        if (position.x < cameraMin.x || position.x > cameraMax.x)
        {
            direction.x = -direction.x;
        }
        if (position.y < cameraMin.y || position.y > cameraMax.y)
        {
            direction.y = -direction.y;
        }
        direction = direction.normalized;

        transform.position = transform.position + direction*Time.deltaTime*moveSpeed;
    }
}
