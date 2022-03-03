using Chess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject mainMenuPanel;
    [SerializeField]
    public GameObject startGamePanel;
    [SerializeField]
    public GameObject settingsPanel;


    [SerializeField]
    public GameObject WhiteSidePanel;
    [SerializeField]
    public GameObject BlackSidePanel;

    [SerializeField]
    public Text mins;
    [SerializeField]
    public Slider settingsMinsSlider;


    [SerializeField]
    public GameObject PieceType1Panel;
    [SerializeField]
    public GameObject PieceType2Panel;
    [SerializeField]
    public GameObject PieceType3Panel;

    [SerializeField]
    public GameObject BoardType1Panel;
    [SerializeField]
    public GameObject BoardType2Panel;
    [SerializeField]
    public GameObject BoardType3Panel;

    public void Start()
    {
        Settings.ReadSettings();

        mins.text = Settings.gameLength.ToString();
    }

    public void StartGame()
    {
        int x = Mathf.RoundToInt(settingsMinsSlider.value);
        Settings.SetGameLength(x);

        SceneManager.LoadScene("GameScene");   
    }
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        startGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowStartGameMenu()
    {
        mainMenuPanel.SetActive(false);
        startGamePanel.SetActive(true);

        if(Settings.side == "white")
        {
            WhiteSidePanel.GetComponent<Image>().color = Color.red;
        }
        else
        {
            BlackSidePanel.GetComponent<Image>().color = Color.red;
        }
    }

    public void ShowSettingsMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);

        if (Settings.piecePath == "Pieces1/")
        {
            PieceType1Panel.GetComponent<Image>().color = Color.red;
        }
        else if (Settings.piecePath == "Pieces2/")
        {
            PieceType2Panel.GetComponent<Image>().color = Color.red;
        }
        else
        {
            PieceType3Panel.GetComponent<Image>().color = Color.red;
        }

        if (Settings.DarkColor.Equals(Settings.DarkBoardTheme1))
        {
            BoardType1Panel.GetComponent<Image>().color = Color.red;
        }
        else if (Settings.DarkColor.Equals(Settings.DarkBoardTheme2))
        {
            BoardType2Panel.GetComponent<Image>().color = Color.red;
        }
        else
        {
            BoardType3Panel.GetComponent<Image>().color = Color.red;
        }

        BoardType1Panel.transform.Find("LightCell").GetComponent<Image>().color = Settings.LightBoardTheme1;
        BoardType1Panel.transform.Find("LightCell2").GetComponent<Image>().color = Settings.LightBoardTheme1;
        BoardType1Panel.transform.Find("DarkCell").GetComponent<Image>().color = Settings.DarkBoardTheme1;
        BoardType1Panel.transform.Find("DarkCell2").GetComponent<Image>().color = Settings.DarkBoardTheme1;

        BoardType2Panel.transform.Find("LightCell").GetComponent<Image>().color = Settings.LightBoardTheme2;
        BoardType2Panel.transform.Find("LightCell2").GetComponent<Image>().color = Settings.LightBoardTheme2;
        BoardType2Panel.transform.Find("DarkCell").GetComponent<Image>().color = Settings.DarkBoardTheme2;
        BoardType2Panel.transform.Find("DarkCell2").GetComponent<Image>().color = Settings.DarkBoardTheme2;

        BoardType3Panel.transform.Find("LightCell").GetComponent<Image>().color = Settings.LightBoardTheme3;
        BoardType3Panel.transform.Find("LightCell2").GetComponent<Image>().color = Settings.LightBoardTheme3;
        BoardType3Panel.transform.Find("DarkCell").GetComponent<Image>().color = Settings.DarkBoardTheme3;
        BoardType3Panel.transform.Find("DarkCell2").GetComponent<Image>().color = Settings.DarkBoardTheme3;
    }

    public void ChangeMins()
    {
        mins.text = settingsMinsSlider.value.ToString();

    }

    public void SaveSettings()
    {
        Settings.SaveSettings();
    }

    public void ChooseBlackSide()
    {
        WhiteSidePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        BlackSidePanel.GetComponent<Image>().color = Color.red;

        Settings.SetSide("black");
    }

    public void ChooseWhiteSide()
    {
        WhiteSidePanel.GetComponent<Image>().color = Color.red;
        BlackSidePanel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        Settings.SetSide("white");
    }

    public void ChoosePiece1Type()
    {
        PieceType1Panel.GetComponent<Image>().color = Color.red;
        PieceType2Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        PieceType3Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        Settings.SetPieceTheme(1);
    }

    public void ChoosePiece2Type()
    {
        PieceType1Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        PieceType2Panel.GetComponent<Image>().color = Color.red;
        PieceType3Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        Settings.SetPieceTheme(2);
    }

    public void ChoosePiece3Type()
    {
        PieceType1Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        PieceType2Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        PieceType3Panel.GetComponent<Image>().color = Color.red;

        Settings.SetPieceTheme(3);
    }

    public void ChooseBoard1Type()
    {
        BoardType1Panel.GetComponent<Image>().color = Color.red;
        BoardType2Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        BoardType3Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        Settings.SetBoardTheme(1);
    }

    public void ChooseBoard2Type()
    {
        BoardType1Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        BoardType2Panel.GetComponent<Image>().color = Color.red;
        BoardType3Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        Settings.SetBoardTheme(2);
    }

    public void ChooseBoard3Type()
    {
        BoardType1Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        BoardType2Panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        BoardType3Panel.GetComponent<Image>().color = Color.red;

        Settings.SetBoardTheme(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
