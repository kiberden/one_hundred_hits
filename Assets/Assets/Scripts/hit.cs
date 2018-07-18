using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{

    [SerializeField]
    GameObject hitPicture;
   /* public CharacterController player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

    }

    private void FlipHero()
    {
        
    }*/

    /*private void Flip(bool flipOnRight)
    {
        this.isShowRight = flipOnRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Instantiate(hitPicture, new Vector2(transform.position.x + 1f, transform.position.y + 2f), Quaternion.identity);
        }

    }
}