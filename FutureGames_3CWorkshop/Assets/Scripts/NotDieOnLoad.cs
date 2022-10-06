using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotDieOnLoad : MonoBehaviour
{
    private void Update()
    {
        RemainAlive();
    }

    public void RemainAlive()
    {
        string whatSceneImIn = SceneManager.GetActiveScene().name;
        Debug.Log(whatSceneImIn);
        if (whatSceneImIn == "MainScene")
        {

            DontDestroyOnLoad(this.gameObject);
        }
    }
}
