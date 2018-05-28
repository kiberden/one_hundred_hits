using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{    public string playerTag;
    public GameObject TurnIs;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == playerTag)
            Destroy(TurnIs);
    }
}