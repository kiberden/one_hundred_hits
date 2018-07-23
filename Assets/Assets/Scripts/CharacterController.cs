using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float dirX, moveSpeed = 5f;
    public int healthPoints = 3;
    public bool isHurting, isDead;
    public bool facingRight = true;
    public Vector3 localScale;

    // Use this for initialization 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    // Update is called once per frame 
    void Update()
    {
        if (IsJumpKeyDown() && !isDead && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 600f);

        SetAnimationState();

        if (!isDead)
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    void FixedUpdate()
    {
        if (!isHurting)
            rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void SetAnimationState()
    {
        if (dirX == 0) {
            anim.SetBool("IsWalk", false);
        }

        if (rb.velocity.y == 0) {
            anim.SetBool("IsJump", false);
            //anim.SetBool("isFalling", false);
        }

        if (Mathf.Abs(dirX) == 5 && rb.velocity.y == 0)
            anim.SetBool("IsWalk", true);

        if (rb.velocity.y > 0) {
            anim.SetBool("IsJump", true);
            anim.SetBool("IsWalk", false);
        }

        if (rb.velocity.y < 0) {
            anim.SetBool("IsJump", false);
        }
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Fire")) {
            healthPoints -= 1;
        }

        if (col.gameObject.name.Equals("Fire") && healthPoints > 0) {
            anim.SetTrigger("isHurting");
            StartCoroutine("Hurt");
        } else {
            dirX = 0;
            isDead = true;
            anim.SetTrigger("isDead");
        }
    }

    IEnumerator Hurt()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;

        if (facingRight)
            rb.AddForce(new Vector2(-200f, 200f));
        else
            rb.AddForce(new Vector2(200f, 200f));

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    private bool IsJumpKeyDown()
    {
        return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow));
    }
}