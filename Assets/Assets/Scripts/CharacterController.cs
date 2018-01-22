using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
	public Transform groundCheck;
	public LayerMask whatIsGround;

	public float jumpForce = 650;
	public float maxSpeed = 10;
	public int damage = 1;
	public int heals = 5;

	private float speed;
	private float groundRadius = 0.2f;
	private bool isPause = false;
	private bool isOnGround = false;
	private bool isShowRight = true;

	private BoxCollider2D weapon;
	private Animator anim; //Анимация

	public string status = "";

	/*debug*/
	public bool isAttack;

	private void Start()
	{
		this.anim = GetComponent<Animator>();// для анимации, но это как обычно
		this.weapon = GameObject.Find("/hero/Sword/").GetComponent<BoxCollider2D>();
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
		this.anim.SetFloat("Speed", Mathf.Abs(this.speed));

		HorizontalMove();
		Attack();
		Jump();
		Pause();

		FlipHero();
	}

	private void FlipHero()
	{
		if (this.speed > 0 && !this.isShowRight) {
			Flip(true);
		} else if (this.speed < 0 && this.isShowRight) {
			Flip(false);
		}
	}

	private void Attack()
	{
		if (Input.GetKeyDown(KeyCode.E) && !this.anim.GetBool("Attack")) {
			//SoundManager.PlaySound ("playerAttack");
			/*debug*/
			//var animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
			//animatorStateInfo.IsName("Player_attack");
			/**/

			this.anim.SetTrigger("Attack");
			//this.StartAttackTrigger();
			HeroMeleAttack.Action(this.weapon, "Enemy", this.damage, false);
		}   
	}

	private void Flip(bool flipOnRight)
	{
		this.isShowRight = flipOnRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void HorizontalMove()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * this.maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	private void Jump()
	{
		if (this.isOnGround && IsJumpKeyDown()) {
			//SoundManager.PlaySound ("playerJump");
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

	private void StartAttackTrigger()
	{
		this.weapon.enabled = true;
	}

	private void CompleteAttackTrigger()
	{
		this.weapon.enabled = false;
		this.anim.ResetTrigger("Attack");
		Debug.Log("end");
	}

	public bool IsAttackStatus()
	{
		return this.anim.GetBool("Attack");
	}



	/*void OnCollisionEnter2D(Collision2D coll) 
	{
		if(coll.gameObject.name == "Khornit")
		{
			Enemy im = coll.gameObject.GetComponent<Enemy>();
			im.Death();
		}
	}*/

}