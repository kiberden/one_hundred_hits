using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieCollider : MonoBehaviour {
    public int numberScene;
    public string playerTag;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == playerTag)
            SceneManager.LoadScene(numberScene);

    }
}
