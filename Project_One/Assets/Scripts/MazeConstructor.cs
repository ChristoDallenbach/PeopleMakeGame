using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug; // will toggle debug displays,

    //materials for generated models. 
    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    private MazeDataGenerator dataGenerator;

    private MazeMeshGenerator meshGenerator;

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
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);
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

}