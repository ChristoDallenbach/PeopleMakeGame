using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private GameObject maze;
    public bool Paused = true;
    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("Procedural Maze");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
            maze.transform.position = new Vector3(maze.transform.position.x, maze.transform.position.y, maze.transform.position.z - speed * Time.deltaTime);
            maze.transform.parent.position = transform.position;
        }
    }

    public void togglePause()
    {
        Paused = !Paused;
    }
}
