using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maromove : MonoBehaviour
{
    public float walkSpeed = 3.0f;      // Walkspeed
    public float wallLeft = 0.0f;       // Define wallLeft
    public float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    float originalX; // Original float value
    Transform transform;
    Rigidbody2D rigidbody;
    bool facingRight;
    

    void Start()
    {
        this.originalX = this.transform.position.x;
       



    }

   void Awake()
    {
        // get a reference to the components we are going to be changing and store a reference for efficiency purposes
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= originalX + wallRight)
        {
            walkingDirection = -1.0f;
        }
        else if (walkingDirection < 0.0f && transform.position.x <= originalX - wallLeft)
        {
            walkingDirection = 1.0f;
        }
        transform.Translate(walkAmount);



    }


    /*  private void FlipHero()
     {
         if (this.walkSpeed > 0 && !this.facingRight)
         {
             Flip(true);
         }
         else if (this.walkSpeed < 0 && this.facingRight)
         {
             Flip(false);
         }
     }
     private void Flip(bool flipOnRight)
     {
         this.facingRight = flipOnRight;
         Vector3 theScale = transform.localScale;
         theScale.x *= -1;
         transform.localScale = theScale;

     }*/

    void flipFacing()
     {
         Vector2 localScale = transform.localScale;
         if (walkSpeed > 0)
             facingRight = false;
         else if (walkSpeed < 0)
             facingRight = true;

         if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
         {
             localScale.x *= -1;
         }

         // update the scale
         transform.localScale = localScale;

     }
}
