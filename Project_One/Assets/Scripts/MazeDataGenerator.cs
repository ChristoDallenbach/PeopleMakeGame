using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    public float placementThreshold;    // chance of empty space

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
        //used by the data generation algorithm to determine whether a space is empty. 
        //This variable is assigned a default value in the class constructor, 
        //but it's made public so that other code can tune the generated maze. 
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                //For every grid cell, the code first checks if the current cell is on the outside of the grid 
                //(that is, if either index is on the array boundaries). If so, assign 1 for wall.
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                }

                //checks if the coordinates are evenly divisible by 2 in order to operate on every other cell. 
                //There is a further check against the placementThreshold value described earlier, 
                //to randomly skip this cell and continue iterating through the array.
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > placementThreshold)
                    {
                        //assigns 1 to both the current cell and a randomly chosen adjacent cell. 
                        //The code uses a series of ternary operators to randomly add 0, 1, or -1 to the array index, 
                        //thereby getting the index of an adjacent cell.
                        maze[i, j] = 1;

                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }
}