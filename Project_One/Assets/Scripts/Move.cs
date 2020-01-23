using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public GameObject maze;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        maze.transform.position = new Vector3(maze.transform.position.x, maze.transform.position.y - speed * Time.deltaTime, maze.transform.position.z);
        maze.transform.parent.position = transform.position;
    }
}
