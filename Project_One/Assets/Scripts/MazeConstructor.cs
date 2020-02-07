using UnityEngine;
using System.Collections;

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
    [SerializeField] private GameObject trap;
    private int level;

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
        level = 1;
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_WalkSpeed = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_StartSpeed;

        DeployTraps();
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
                else if (maze[i,j] == 1)
                {
                    msg += "==";
                }
                else
                {
                    msg += "////";
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

    private void DeployTraps()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 2)
                {
                    Instantiate(trap, new Vector3(i, 0, j), trap.transform.rotation);
                }
            }
        }
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
                    player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                    player.transform.position = new Vector3(startCol*3.75f, 1, startRow*3.75f);
                    StartCoroutine(Enable(0.25f));
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
                if (maze[i, j] == 0 && Random.Range(0.0f,1.0f) > 0.95f)
                {
                    goalRow = i;
                    goalCol = j;
                    end.transform.position = new Vector3(goalCol * 3.75f, 1, goalRow * 3.75f);
                    return;
                }
            }
        }
        FindGoalPosition();//Failsafe to restart goal selecting if the method never chooses a goal
    }

    public void DisposeOldMaze(int level)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
        this.level = level;
    }

    private IEnumerator Enable(float time)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
    }
}