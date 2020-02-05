using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug; // will toggle debug displays,

    //materials for generated models. 
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject end;

    //The player position, in relation to the maze
    private int playerX, playerZ;

    private MazeDataGenerator dataGenerator;

    private MazeMeshGenerator meshGenerator;

    public float hallWidth
    {
        get; private set;
    }
    public float hallHeight
    {
        get; private set;
    }

    public int startRow
    {
        get; private set;
    }
    public int startCol
    {
        get; private set;
    }

    public int goalRow
    {
        get; private set;
    }
    public int goalCol
    {
        get; private set;
    }

    // makes it read-only outside this class.
    public int[,] data
    {
        get; private set;
    }

    // initializes data with a 3 by 3 array of ones surrounding zero. 
    //1 means “wall” while 0 means “empty”, so this default grid is a walled-in room.
    void Awake()
    {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();
        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        hallWidth = 4;
        hallHeight = 4;
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        FindStartPosition();
        FindGoalPosition();
        DisplayMaze();
    }

    void OnGUI()
    {
        //checks if debug displays are enabled.
        if (!showDebug)
        {
            return;
        }

        //Initialize several local variables: a local copy of the stored maze, 
        //the maximum row and column, and a string to build up.
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        //checks the stored value and appends either "...." or "==" depending on if the value is zero
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

        //prints out the built-up string.
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };
    }

    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    player.transform.position = new Vector3(startCol*hallWidth, 0, startRow*hallWidth);
                    return;
                }
            }
        }
    }

    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        // loop top to bottom, right to left
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    end.transform.position = new Vector3(goalCol * 4-8, 1, goalRow * 4-8);
                    return;
                }
            }
        }
    }

    void Update(){
        playerX = (int)((4+player.transform.position.x) / 4);
        playerZ = (int)((2+player.transform.position.z) / 4);

        if (playerX == goalRow && playerZ == goalCol)
        {
            Debug.Log("You Win!");
        }
        else
            Debug.Log(playerX + ", " + playerZ);
    }
}