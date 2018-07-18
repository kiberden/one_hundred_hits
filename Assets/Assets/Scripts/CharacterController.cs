using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CharacterController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private HealsBar healsbar;
    public Enemy MyTransform;
    // private CharacterController player;
    /*  private Enemy enemy;*/


    // Переменные c плавающей запятой: сила прыжка, скорость, максимальная скорость, и граунд радиус(?)
    public float jumpForce = 650;
    public float maxSpeed = 10;
    private float speed;
    private float groundRadius = 0.2f;
    public float jumpBack; //knockback after damage      

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
    private bool isPause = false;
    private bool isOnGround = false;
    private bool isShowRight = true;
    // public bool isAttack;
    ///bool damaged;

    // Подключаемые 
    //private BoxCollider2D weapon;
    private Animator anim; //Анимация
    private Rigidbody2D rb2d; // Для отталкивания от врагов
    public string status = "";

    
    /*debug*/

    private void Awake()
    {

        healsbar = FindObjectOfType<HealsBar>();

    } // для отображения жизней

    private void Start()
    {
        InvokeRepeating("nCollisionEnter2D", 0f, 5.0f);
        this.anim = GetComponent<Animator>();// для анимации, но это как обычно
       // this.weapon = GameObject.Find("/hero/Sword/").GetComponent<BoxCollider2D>();
        /*enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();*/
        
        
   
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

        {
            if (heals <= 0)
            {
                heals = 0;
                //Death();
            }

        }// смерть

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

    }

    private void Forceback(Vector3 dir)
      {
          rb2d.AddForce(dir.normalized * 8.0F, ForceMode2D.Impulse);
      }

    /// Начало. Отложенна функция отброса
   /* public IEnumerator Knockback(float knockDur, float knockPwr, Vector3 knockbakcDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(x: knockbakcDir.x * -100, y: knockbakcDir.y * knockPwr, z: transform.position.z));

        }

        yield return 0;



    } */

    /// Конец. Отложенна функция отброса

    private void Attack()
      {
          if (Input.GetKeyDown(KeyCode.E) && !this.anim.GetBool("Attack"))
          {
              SoundManager.PlaySound("playerAttack");
              /*debug*/
    var animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            animatorStateInfo.IsName("Player_attack");
            /**/

            this.anim.SetTrigger("Attack");
            //this.StartAttackTrigger();
            //HeroMeleAttack.Action(this.weapon, "Enemy", this.damage, false);
        }
    }

    /// Начало метода поворота
    private void FlipHero()
    {
        if (this.speed > 0 && !this.isShowRight)
        {
            Flip(true);
        }
        else if (this.speed < 0 && this.isShowRight)
        {
            Flip(false);
        }
    }
    private void Flip(bool flipOnRight)
    {
        this.isShowRight = flipOnRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    /// Конец метода поворота

    /// Начало метода передвижения
    private void HorizontalMove()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * this.maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
    /// Конец метода передвижения

    /// Начало метода прыжка
    private void Jump()
    {
        if (this.isOnGround && IsJumpKeyDown())
        {
            SoundManager.PlaySound("playerJump");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, this.jumpForce));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
        }
    }
    /// Конец метода прижка
    
    /// Начало блока паузы
    private void Pause()
    {
        if (IsPauseKeyDown())
        {
            if (!this.isPause)
            {
                PauseOn();
            }
            else
            {
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
    /// Конец блока меню паузы

    /// Начало отброса - сила и направление
    private void Knock()
    {
        transform.Translate(Vector2.up * jumpBack);
    }
    /// Конец отброса- сила и направление

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
    /*private void StartAttackTrigger()
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
    }*/
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


    }
     /*void OnTriggerEnter2D(Collider2D col)
      {
          if (col.CompareTag("Enemy"))
          {


              anim.SetTrigger("Damage_trig");
              //StartCoroutine(Dam(0.2f));
          }


      }*/
    // Конец блока тригеров и коллизий

}