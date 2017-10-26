using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public float attackSpeed = 1;
    public float jumpForce = 400;
    public float maxSpeed = 10;
    public int damage = 1;
    public int heals = 1;

    private float speed;
    private float groundRadius = 0.2f;
    private bool isPause = false;
    private bool isOnGround = false;

	//Анимация
	Animator anim;

	private void Start()
	{
		// для анимации, но это как обычно
		anim = GetComponent<Animator>();
    }

    /**
     * update every 1 frames
     */
    private void FixedUpdate()
    {

        this.isOnGround = Physics2D.OverlapCircle(
                this.groundCheck.position,
                this.groundRadius,
                this.whatIsGround
            );
        this.speed = Input.GetAxisRaw("Horizontal");

	}

    private void Update()
    {
        HorizontalMove();
        Jump();
        Pause();
    }

	private void HorizontalMove()
	{
        GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    
	
	
	}



    private void Jump()
    {
        if (this.isOnGround && IsJumpKeyDown()) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, this.jumpForce));
        }
    }

    private void Pause()
    {
        if (IsPauseKeyDown()) {
            if (!this.isPause) {
                PauseOn();
            } else {
                PauseOff();
            }
        }
    }

    private void PauseOn()
    {
        Time.timeScale = 0;
        this.isPause = true;
    }

    private void PauseOff()
    {
        Time.timeScale = 1;
        this.isPause = false;
    }

    private bool IsPauseKeyDown()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    private bool IsJumpKeyDown()
    {
        return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow));
    }
}