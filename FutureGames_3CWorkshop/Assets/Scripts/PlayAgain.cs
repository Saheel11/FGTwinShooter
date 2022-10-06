using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class PlayAgain : MonoBehaviour
{ 

    public void ReloadGame()
    {      
        SceneManager.LoadScene("MainScene");
    }   


}
