using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this script keeps track of player score and also generates new mazes
/// also to add:
/// lose/win state
/// </summary>
[RequireComponent(typeof(MazeConstructor))]               
// ensures that a MazeConstructor component will also be added when you add this script to a GameObject.


public class GameController : MonoBehaviour
{
    private MazeConstructor generator;
    [SerializeField] private int height, width;
    public int level = 1; //which level the player is currently on, starting at 1 and rising

    public Text Text;

    public GameObject PauseMenu;
    public GameObject StartButton;
    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        // A private variable that stores a reference returned by the GetComponent()

        generator.GenerateNewMaze(height, width);
        //parameters dictate how large to make the maze. While they aren't being used quite yet, 
        //these size parameters determine the number of rows and columns in the grid respectively.
        Text = GameObject.Find("Text").GetComponent<Text>();

        Text.text = "Level: "+level;
        PauseMenu = GameObject.Find("Panel");
        StartButton = GameObject.Find("StartButton");
       }

    //function: NextLevel. call when player reaches maze goal. raise level, generate new maze.
    public void NextLevel()
    {
        //increment player level
        level++;

        generator.DisposeOldMaze(level);
        //generate new maze
        generator.GenerateNewMaze(height, width);
        //update UI
        Text.text = "Level: "+level;
        
    }

    //function: PlayerLose. call when player reaches the lose state by hitting obstacle or wall. reset score to 0 and reset maze
    public void PlayerLose()
    {
        //reset player level
        level = 1;
        //reset player's speed

        //generate new maze
        generator.GenerateNewMaze(height, width);

        //update UI
        Text.text = "Level: " + level;
    }

    public void DisablePaused()
    {
        //reverse the visibility of the panels image
        PauseMenu.GetComponent<Image>().enabled = false;
        //get the button
        //reverse visability of button's image
        StartButton.SetActive(false);
        
    }

}