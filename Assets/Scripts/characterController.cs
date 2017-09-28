using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour
{
	public float maxSpeed = 10;
    public float jumpForce = 400;

    private float speed;
    private bool isOnGround = false;

	private void Start()
	{

    }

    /**
     * Âûחûגאועסÿ םא ךאזהûי פנויל
     */
    private void FixedUpdate()
    {
        this.speed = Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        HorizontalMove();
        Jump();
    }

	private void HorizontalMove()
	{
        GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, this.jumpForce));
        }
    }
}