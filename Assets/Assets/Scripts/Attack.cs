using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    private CharacterController player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Maroder.isAttacking = true;
            //player.Damag(1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Maroder.isAttacking = false;
        }
    }
}
