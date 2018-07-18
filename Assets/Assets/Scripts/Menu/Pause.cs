using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    [HideInInspector]
   public bool isPaused;
    [SerializeField]
    private KeyCode pausebutton;
    [SerializeField]
    private GameObject panelPause;


	// Use this for initialization
	void Start () {
        Cursor.visible = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(pausebutton))
        {
            isPaused = !isPaused;
            Cursor.visible = true;
        }
		if (isPaused)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;      
        }
        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
   
	}
}
