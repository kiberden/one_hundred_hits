using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public LayerMask enemyMask;
	public float speed = 1;
	public Rigidbody2D myBody;
	public Transform MyTransform;
    public Transform target;
	float myWidth, myHeight;

    public int hits = 5;
    public Animator enemyAnimator;

    public bool isAlive = true;
    public bool isPatrol = true;

    /*debug*/
    public bool SawHero;
    public float Range;
    public float move;
    public bool heroAttack;

    void Start ()
	{
		this.MyTransform = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
        this.target = GameObject.FindGameObjectWithTag("Player").transform;

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myWidth = mySprite.bounds.extents.x;

		myHeight = mySprite.bounds.extents.y;
		this.enemyAnimator = GetComponent<Animator>();

        this.enemyAnimator.SetBool("Walk", true);
    }

	void FixedUpdate ()
	{
        if (this.SawHero = this.IsSawEnermy()) {
            this.isPatrol = false;

            this.Range = Vector2.Distance(this.MyTransform.position, this.target.position);

            if (Range >= 8f) {
                if (this.IsGrounded() || !this.IsBlocked()) {
                    this.Walk();
                }
            } else if (Range < 8f && Range > 3f) {
                if (this.IsGrounded() || !this.IsBlocked()) {
                    this.Charge();
                }
            } else if (Range <= 3f) {
                //атака или блок
                CharacterController Hero = this.target.gameObject.GetComponent<CharacterController>();
                this.heroAttack = Hero.IsAttackStatus();
            }
        } else {
            this.isPatrol = true;
            this.WalkAround();
        }

        if (this.hits <= 0) {
            this.Death();
        }
    }

    private bool IsSawEnermy()
    {
        Vector2 SeeLinePosition = this.MyTransform.position.toVector2() - this.MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        /*debug only*/Debug.DrawLine(SeeLinePosition, SeeLinePosition - MyTransform.right.toVector2() * 16f, Color.green);
        return Physics2D.Linecast(SeeLinePosition, SeeLinePosition - MyTransform.right.toVector2() * 16f, LayerMask.GetMask("Hero"));
    }

    public void WalkAround()
    {
        if (this.isPatrol) {
            if (!this.IsGrounded() || this.IsBlocked()) {
                Vector3 currRot = MyTransform.eulerAngles;
                currRot.y += 180;
                MyTransform.eulerAngles = currRot;
            }

            //Always move forward
            this.Walk();
        }
    }

    private void Walk(float speedMultiplier = 1)
    {
        Vector2 myVel = this.myBody.velocity;
        myVel.x = -this.MyTransform.right.x * this.speed * speedMultiplier;

        this.myBody.velocity = myVel;
    }

    private void Charge()
    {
        this.Walk(3f);
    }

    private bool IsBlocked()
    {
        Vector2 lineCastPos = MyTransform.position.toVector2() - MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos - MyTransform.right.toVector2(), Color.yellow);

        return Physics2D.Linecast(lineCastPos, lineCastPos - MyTransform.right.toVector2(), enemyMask);
    }

    private bool IsGrounded()
    {
        Vector2 lineCastPos = MyTransform.position.toVector2() - MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos + new Vector2(0, -4), Color.red);

        return Physics2D.Linecast(lineCastPos, lineCastPos + new Vector2(0, -4), enemyMask);
    }

	public void Death()
	{
		if (isAlive) {
			this.enemyAnimator.SetBool("isAlive", false);
			speed = 0;
			isAlive = false;
            //GameObject.DestroyObject(this.gameObject);
        }
	}

}