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
    public Text[] answerButtons;
    [SerializeField] private GameObject HUDpanelG2;
    [SerializeField] private GameObject PausepanelG2;
    [SerializeField] private GameObject CorrectAnswerPanelG2;
    [SerializeField] private GameObject WrongAnswerPanelG2;
    [SerializeField] private GameObject NoProductLeftPanelG2;
    [SerializeField] private GameObject blurry;
    [SerializeField] private GameObject GameTwoPreWindow;
    [SerializeField] private Text score;
    public Text highScoreGame2;

    public Text nameButton1;
    public Text nameButton2;
    public Text nameButton3;

    public Text noProductLeft;

    public delegate void OnRightAnswer();
    public static event OnRightAnswer SetNextProduct;

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else if (Main != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScoreTextMenu.text = PlayerPrefs.GetInt("HighScore").ToString();
        highScoreGame2.text = PlayerPrefs.GetInt("HighScoreGame2").ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Game1
    public void UpdateSpeedMultiplier(float value)
    {
        speedMultiplierText.text = "Speed:  " + value.ToString();
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
    public void StartGameTwo()
    {
        GameTwoPreWindow.SetActive(true);
        StartCoroutine(TimerWindowGameTwo());
    }

    IEnumerator TimerWindowGameTwo()
    {
        yield return new WaitForSeconds(5);
        GameManager.Main.LoadScene(2);
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
        SoundManager.Main.StopBackgroundMusic();
        GameManager.Main.SaveHighScore();
        GameManager.Main.LoadMenuScene();
    }
    #endregion
    #region Game2
    public void ChangeUIStateGame2(UIStateGame2 newUIstate)
    {
        DeactivateAllPanelsGame2();
        switch (newUIstate)
        {
            case UIStateGame2.HUDpanel:
                HUDpanelG2.SetActive(true);
                break;
            case UIStateGame2.PausePanel:
                PausepanelG2.SetActive(true);
                break;
            case UIStateGame2.StartMenuPanel:
                StartMenuPanel.SetActive(true);
                break;
            case UIStateGame2.CorrectAnswerPanel:
                CorrectAnswerPanelG2.SetActive(true);
                break;
            case UIStateGame2.WrongAnswerPanel:
                WrongAnswerPanelG2.SetActive(true);
                break;
            case UIStateGame2.NoProductLeftPanel:
                NoProductLeftPanelG2.SetActive(true);
                break;
        }
    }

    private void DeactivateAllPanelsGame2()
    {
        HUDpanelG2.SetActive(false);
        PausepanelG2.SetActive(false);
        StartMenuPanel.SetActive(false);
        CorrectAnswerPanelG2.SetActive(false);
        WrongAnswerPanelG2.SetActive(false);
        NoProductLeftPanelG2.SetActive(false);
    }

    public void SetBlurry(bool state)
    {
        if (state == true) blurry.SetActive(true);
        else blurry.SetActive(false);
    }

    public void ActivatePausePanelG2()
    {
        ChangeUIStateGame2(UIStateGame2.PausePanel);
    }
    public void ContinueGame()
    {
        ChangeUIStateGame2(UIStateGame2.HUDpanel);
    }
    public void ReturnToMenu()
    {
        SoundManager.Main.StopBackgroundMusic();
        GameManager.Main.SaveHighScoreGame2();
        GameManager.Main.LoadMenuScene();
    }

    public void ActivateCorrectAnswerPanel()
    {
        ChangeUIStateGame2(UIStateGame2.CorrectAnswerPanel);
        SoundManager.Main.ChooseSound(SoundType.cheer);
        SetBlurry(false);
        GameManager.Main.Score += 100;
        StartCoroutine(NextProduct());
    }

    public void ActivateWrongAnswerPanel()
    {
        GameManager.Main.Life--;
        ChangeUIStateGame2(UIStateGame2.WrongAnswerPanel);
        SoundManager.Main.ChooseSound(SoundType.oohh);
        SetBlurry(false);
        GameManager.Main.Score -= 50;
        StartCoroutine(NextProduct());

    }

    IEnumerator NextProduct()
    {
        if (GameManager.Main.gameStateGame2 == GameStateGame2.playing)
        {
            yield return new WaitForSeconds(2);
            ChangeUIStateGame2(UIStateGame2.HUDpanel);
            SetNextProduct.Invoke();
        }
    }

    public void UpdateScoreGame2()
    {
        score.text = "Punkte:  " + GameManager.Main.Score.ToString();
    }

    public void Button1()
    {
        if (GameManager.Main.actualProductName == nameButton1.text)
        {
            ActivateCorrectAnswerPanel();
        }
        else
        {
            ActivateWrongAnswerPanel();
        }
    }
    public void Button2()
    {
        if (GameManager.Main.actualProductName == nameButton2.text)
        {

            ActivateCorrectAnswerPanel();
        }
        else
        {
            ActivateWrongAnswerPanel();
        }

    }
    public void Button3()
    {
        if (GameManager.Main.actualProductName == nameButton3.text)
        {
            ActivateCorrectAnswerPanel();
        }
        else
        {
            ActivateWrongAnswerPanel();
        }
    }

   
    #endregion
}

public enum UIState
{
    HUDpanel,
    PausePanel,
    StartMenuPanel,
    DeathPanel
}
public enum UIStateGame2
{
    HUDpanel,
    PausePanel,
    StartMenuPanel,
    CorrectAnswerPanel,
    WrongAnswerPanel,
    NoProductLeftPanel
}