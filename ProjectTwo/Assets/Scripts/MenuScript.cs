using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    startScreen,
    Game,
    Paused,
    Lose,
}
public class MenuScript : MonoBehaviour
{
    //properties
    //reference to pause panel and start menu panel, and the panel with the gamemode UI
    public GameObject PauseScreen;
    public GameObject StartScreen;
    public GameObject GameUI;
    //bool for paused
    public bool paused;
    //public enum for gamestate ... might move to manager later
    public enum GameStates
    {
        startScreen,
        Game,
        Paused,
        Lose,
    }

    public GameStates CurrentState;


    // Start is called before the first frame update
    void Start()
    {
        //
        //paused = true;
        //Time.timeScale = 1;
        //
        //OpenStartMenu();
        paused = false;
        Time.timeScale = 1;

        UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        //make any updates to game ui if in game mode
    }

    //function to pull up pause screen
    public void Pause()
    {
        
        CurrentState = GameStates.Paused;       
        paused = true;
        Time.timeScale = 0;
        setUI();
    }

    //function to close pause screen
    public void UnPause()
    {
        paused = false;
        CurrentState = GameStates.Game;
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        setUI();
    }

    //function to pull up start screen
    public void OpenStartMenu()
    {
        paused = true;
        CurrentState = GameStates.startScreen;
        Time.timeScale = 0;
        setUI();
    }
    //function to start game from start menu
    public void StartGame()
    {
        CurrentState = GameStates.Game;
        Time.timeScale = 1;
        paused = false;
        setUI();
    }


    //function to switch between 180 and 360
    public void Toggle360(bool t)
    {

    }

    // setup the UI based on game state
    public void setUI()
    {
        switch (CurrentState)
        {
            case GameStates.Game:
                GameUI.SetActive(true);
                StartScreen.SetActive(false);
                PauseScreen.SetActive(false);
                break;
            case GameStates.Paused:
                GameUI.SetActive(false);
                StartScreen.SetActive(false);
                PauseScreen.SetActive(true);
                break;
            case GameStates.startScreen:
                GameUI.SetActive(false);
                StartScreen.SetActive(true);
                PauseScreen.SetActive(false);
                break;
            case GameStates.Lose:
                GameUI.SetActive(false);
                StartScreen.SetActive(false);
                PauseScreen.SetActive(true);
                break;
            default:
                print("error. gamestate in menu stript reached default");
                break;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }


}
