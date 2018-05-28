using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public int numberScene;
    public string playerTag;

 
    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("В тригере");
        if (other.tag == playerTag)
        {
            SceneManager.LoadScene(numberScene);
        }
            
    }

}
