using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public GameObject gameController;
    public Slider meterSlider;
    public Slider cooldownSlider;
    public TextMeshProUGUI cooldownText;



    
    private void Update()
    {
        cooldownText.text = gameController.GetComponent<GameController>().timer.ToString("f0");
        meterSlider.value = player.GetComponent<PlayerStats>().meter;
        cooldownSlider.value = player.GetComponent<PlayerStats>().dashCooldown;
    }
}
