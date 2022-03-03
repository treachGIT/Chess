using Chess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    GameController controller;
    void Start()
    {
        controller = new GameController();
        controller.StartGame();
    }

    
}
