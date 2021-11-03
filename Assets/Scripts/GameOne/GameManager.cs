using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Main;

    private GameState gameState;
    public GameState GameState { get => gameState; }

    private bool touchInput = false;
    public bool TouchInput { get => touchInput; set => touchInput = value; }

    public int actualScene = 0;

    private int highScore = 0;
    public int HighScore { get => highScore; set { highScore = value; } }

    public int SavedHighScore;

    private int bananas = 0;
    private int apples = 0;
    private int cherries = 0;
    private int kiwis = 0;
    private int melons = 0;
    private int pineapple = 0;

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("StartMenu").isLoaded)
        {
            ChangeGameState(GameState.StartMenu);
        }
        else gameState = GameState.playing;
        SavedHighScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(GameState newGameState)
    {
        Time.timeScale = 1;
        switch (newGameState)
        {
            case GameState.playing:
                UIManager.Main.ChangeUIState(UIState.HUDpanel);
                break;
            case GameState.Pause:
                UIManager.Main.ChangeUIState(UIState.PausePanel);
                Time.timeScale = 0;
                break;
            case GameState.StartMenu:
                UIManager.Main.ChangeUIState(UIState.StartMenuPanel);
                break;
            case GameState.Death:
                UIManager.Main.ChangeUIState(UIState.DeathPanel);
                break;
            default:
                break;
        }
        gameState = newGameState;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
        actualScene = 0;
        ChangeGameState(GameState.StartMenu);
        UIManager.Main.highScoreTextMenu.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void LoadScene(int i)
    {
        ResetValues();
        SceneManager.LoadScene(i);
        actualScene = i;
        ChangeGameState(GameState.playing);
    }

    public void FruitAdder(FruitType fruit)
    {
        switch (fruit)
        {
            case FruitType.Banana:
                bananas++;
                UpdateFruitUI();
                break;
            case FruitType.Apple:
                apples++;
                UpdateFruitUI();
                break;
            case FruitType.Cherries:
                cherries++;
                UpdateFruitUI();
                break;
            case FruitType.Kiwi:
                kiwis++;
                UpdateFruitUI();
                break;
            case FruitType.Melon:
                melons++;
                UpdateFruitUI();
                break;
            case FruitType.Pineapple:
                pineapple++;
                UpdateFruitUI();
                break;
        }
    }
    public void UpdateFruitUI()
    {
        UIManager.Main.bananaText.text = bananas.ToString();
        UIManager.Main.appleText.text = apples.ToString();
        UIManager.Main.cherryText.text = cherries.ToString();
        UIManager.Main.kiwiText.text = kiwis.ToString();
        UIManager.Main.melonsText.text = melons.ToString();
        UIManager.Main.pineAppleText.text = pineapple.ToString();
    }

    public void UpdateScoreDistance()
    {
        UIManager.Main.scoreText.text = "Score:  " + highScore.ToString();
    }
    public void UpdateScoreFruits(int points)
    {
        HighScore += points;
        UIManager.Main.scoreText.text = highScore.ToString();
    }

    public void ResetValues()
    {
        HighScore = 0;
        apples = 0;
        bananas = 0;
        cherries = 0;
        kiwis = 0;
        melons = 0;
        pineapple = 0;
    }

    public void SaveHighScore()
    {
        if (HighScore > SavedHighScore)
        {
            SavedHighScore = HighScore;
            PlayerPrefs.SetInt("HighScore", GameManager.Main.HighScore);
        }
    }
}

public enum GameState
{
    playing,
    Pause,
    StartMenu,
    Death,
    LevelFinished
}
