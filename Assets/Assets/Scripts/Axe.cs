using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    private CharacterController player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

    }


    /*void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            player.Damage(3);

            StartCoroutine(player.Knockback(10, 350, player.transform.position));

        }
    }
}

*/

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //                  player.Damag(1);
    //    }
    //}
}