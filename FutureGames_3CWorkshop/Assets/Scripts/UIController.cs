using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject player;
    public Slider meterSlider;
    public Slider cooldownSlider;

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        meterSlider.value = player.GetComponent<PlayerStats>().meter;
        cooldownSlider.value = player.GetComponent<PlayerStats>().dashCooldown;
    }
}
