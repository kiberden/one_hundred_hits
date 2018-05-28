using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_maneger : MonoBehaviour {

    public void PlayGame (GameObject obj)
    {
        obj.GetComponent<Pause>().isPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
