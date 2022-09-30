using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public Slider meterSlider;
    public Slider cooldownSlider;
    public TextMeshProUGUI cooldownText;
    
    private void Update()
    {
        cooldownText.text = player.GetComponent<PlayerStats>().dashCooldown.ToString();
        meterSlider.value = player.GetComponent<PlayerStats>().meter;
        cooldownSlider.value = player.GetComponent<PlayerStats>().dashCooldown;
    }
}
