using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Textremain : MonoBehaviour
{


    public GameController controller;

    public float winTimer;
    public TextMeshProUGUI timeWinString;

    private void Start()
    {
        GameObject[] gameController = GameObject.FindGameObjectsWithTag("GameController");
        foreach(GameObject gc in gameController)
        {

            controller = gc.GetComponent<GameController>();

            
        }

        winTimer = controller.GetTimer();
        timeWinString.text = "GG, your time was " + winTimer.ToString("f0");

        Destroy(controller.gameObject);
    }




}
