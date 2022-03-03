using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerValue;
    [SerializeField]
    public GameView view;

    private float gameTime = 3600;
    private bool stop = true;
    public bool gameTimeEnd = false;

    private void Awake()
    {
        timerValue = GetComponent<Text>();
        timerValue.text = "00:00";  
    }

    private void Update()
    {
        if (gameTime > 0 && stop == false)
        {
            gameTime -= Time.deltaTime;
            DisplayTime(gameTime);
        }

        if (gameTime < 0 && stop == false)
        {
            foreach (GameObject piece in view.whitePieces)
            {
                piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }

            foreach (GameObject piece in view.blackPieces)
            {
                piece.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            stop = true;
            gameTimeEnd = true;
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimer(float time)
    {
        gameTime = time;
        DisplayTime(gameTime - 1);
    }

    public void StartTimer()
    {
        stop = false;
    }

    public void StopTimer()
    {
        stop = true;
    }


}
