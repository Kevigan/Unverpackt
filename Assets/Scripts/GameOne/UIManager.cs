using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Main;

    [Header("Panel")]
    [SerializeField] private GameObject HUDpanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject StartMenuPanel;
    [Header("FruitsText")]
    public Text bananaText;
    public Text cherryText;
    public Text kiwiText;
    public Text melonsText;
    public Text pineAppleText;
    public Text appleText;
    [Header("Score")]
    public Text scoreText;
    public Text highScoreTextMenu;
    public Text speedMultiplierText;
    [Header("SecondGame")]
    public Text answerButton1;
    public Text answerButton2;
    public Text answerButton3;


    private void Awake()
    {
        if(Main == null)
        {
            Main = this;
        }
        else if(Main != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreTextMenu.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpeedMultiplier(float value)
    {
        speedMultiplierText.text ="Speed:  "+ value.ToString();
    }

    public void ChangeUIState(UIState newState)
    {
        DeactivateAllPanels();
        switch (newState)
        {
            case UIState.HUDpanel:
                HUDpanel.SetActive(true);
                break;
            case UIState.PausePanel:
                PausePanel.SetActive(true);
                break;
            case UIState.StartMenuPanel:
                StartMenuPanel.SetActive(true);
                break;
            case UIState.DeathPanel:
                DeathPanel.SetActive(true);
                break;
        }
    }

    private void DeactivateAllPanels()
    {
        HUDpanel.SetActive(false);
        PausePanel.SetActive(false);
        DeathPanel.SetActive(false);
        StartMenuPanel.SetActive(false);
    }

    public void ActivatePausePanel()
    {
        GameManager.Main.ChangeGameState(GameState.Pause);
    }
   
    public void ActivateHUDPanel()
    {
        GameManager.Main.ChangeGameState(GameState.playing);
    }

    public void StartGame(int i)
    {
        GameManager.Main.LoadScene(i);
    }
    public void ReloadActualScene()
    {
        GameManager.Main.LoadScene(GameManager.Main.actualScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitLevel()
    {
        GameManager.Main.SaveHighScore();
        GameManager.Main.LoadMenuScene();
    }
    
   
}

public enum UIState
{
    HUDpanel,
    PausePanel,
    StartMenuPanel,
    DeathPanel
}