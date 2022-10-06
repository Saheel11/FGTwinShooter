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

    public Image[] ammoList;

    public bool win;
    private void Update()
    {
        if (!win)
        {
            cooldownText.text = gameController.GetComponent<GameController>().timer.ToString("f0");
            meterSlider.value = player.GetComponent<PlayerStats>().meter;
            cooldownSlider.value = player.GetComponent<PlayerStats>().dashCooldown;
        }
        
        /*{
            cooldownText.enabled = false;
            meterSlider.enabled = false;
            cooldownSlider.enabled = false;

            foreach (Image ammo in ammoList)
            {
                ammo.enabled = false;
            }
                */

        
        

       
    }


    public void ammoShow(int whichAmmo)
    {


        for(int i = 0; i < ammoList.Length; i++)
        {

            int countAmmo = whichAmmo - 1;

            if(countAmmo >= i)
            {
                ammoList[i].enabled = true;


            }
            else
            {
                ammoList[i].enabled = false;


            }


        }


    }

    

}
