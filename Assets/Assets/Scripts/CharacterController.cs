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
<<<<<<< HEAD
=======
    public float jumpBack; //knockback after damage    
    public bool IsJumpNow = false;
    public Component weapon;

    // Переменные целые: Урон и Здоровье
    public int damage = 1; // урон наносимый
    [SerializeField]
    public static int heals = 5; // количество жизней

    public int Heals // для отображения жизней 1
    {
        get { return heals; }
        set
        {
            if (value < 5) heals = value;
            healsbar.Refresh();
        }
    }

    // Булевые значения
>>>>>>> 5f61ee9... поправляем
    private bool isPause = false;
    private bool isOnGround = false;
    private bool isShowRight = true;
    
    private BoxCollider2D weapon;
    private Animator anim; //Анимация

    public string status = "";

    /*debug*/
    public bool isAttack;

    private void Start()
<<<<<<< HEAD
	{
		this.anim = GetComponent<Animator>();// для анимации, но это как обычно
        this.weapon = GameObject.Find("/hero/Sword/").GetComponent<BoxCollider2D>();
=======
    {
        InvokeRepeating("nCollisionEnter2D", 0f, 5.0f);
        this.anim = GetComponent<Animator>();// для анимации, но это как обычно
        //this.weapon = GameObject.Find("/hero/Sword/").GetComponent<BoxCollider2D>();
        /*enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();*/
>>>>>>> 5f61ee9... поправляем
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
<<<<<<< HEAD
=======

        {
            if (heals <= 0)
            {
                heals = 0;
                //Death();
            }

        }// смерть

        IsJumpNow = IsJumpKeyDown();
    }

    public IEnumerator Dam(float timerdur)
      {
        /*  MyTransform = FindObjectOfType<Enemy>();
          Vector3 dir = (groundCheck.transform.position - MyTransform.transform.position);
          Forceback(dir); */

        float timer = 0;


        while (timerdur > timer)
        {
            timer += Time.deltaTime;
            transform.Translate(Vector2.left * jumpBack);
        }
        yield return new WaitForSeconds(10.0f);
        //yield return 0;

>>>>>>> 5f61ee9... поправляем
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
<<<<<<< HEAD
    {
        if (Input.GetKeyDown(KeyCode.E) && !this.anim.GetBool("Attack")) {
            /*debug*/
            //var animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            //animatorStateInfo.IsName("Player_attack");
=======
      {
          if (Input.GetKeyDown(KeyCode.E) && !this.anim.GetBool("Attack"))
          {
              SoundManager.PlaySound("playerAttack");
              /*debug*/
                var animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            animatorStateInfo.IsName("Player_attack");
>>>>>>> 5f61ee9... поправляем
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

<<<<<<< HEAD
=======
    ///Начало блока нанесение урона
    public void Damag(int dmg)
    {

        Heals -= dmg;


    }
    ///Конец блока нанесение урона

    /// Начало смерть 
     private void Death()
       {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }
    /// Конец смерть


    // Начало блока тригеров и коллизий
>>>>>>> 5f61ee9... поправляем
    private void StartAttackTrigger()
    {
        BoxCollider2D weaponStat = this.weapon.GetComponent<BoxCollider2D>();
        weaponStat.enabled = true;
    }

    private void CompleteAttackTrigger()
    {
        BoxCollider2D weaponStat = this.weapon.GetComponent<BoxCollider2D>();
        weaponStat.enabled = false;
        this.anim.ResetTrigger("Attack");
        Debug.Log("end");
    }

    public bool IsAttackStatus()
    {
        return this.anim.GetBool("Attack");
<<<<<<< HEAD
=======
    }
    void OnCollisionEnter2D(Collision2D coll)


    {
       /* if (coll.gameObject.name == "Khornit")
          {
              Enemy im = coll.gameObject.GetComponent<Enemy>();
              im.Death();
          }
        */
        if (coll.gameObject.tag == "Enemy")
          {
           Heals--; // заменил h на H чтобы отнимались сердечки                 

           anim.SetTrigger("Damage_trig");
           StartCoroutine(Dam(0.2f));
           



        }


>>>>>>> 5f61ee9... поправляем
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