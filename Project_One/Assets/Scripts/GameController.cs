using System;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]               
// ensures that a MazeConstructor component will also be added when you add this script to a GameObject.

public class GameController : MonoBehaviour
{
    private MazeConstructor generator;

    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        // A private variable that stores a reference returned by the GetComponent()

        generator.GenerateNewMaze(31, 31);
        //parameters dictate how large to make the maze. While they aren't being used quite yet, 
        //these size parameters determine the number of rows and columns in the grid respectively.

    }
}