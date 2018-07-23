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
    public bool isShield = false;
    public bool isAttack = false;

    private Animator HeroAnimator;

    //debug
    public bool SawHero;
    public float Range;
    public float move;
    public bool heroAttack;

    private CharacterController player;


    /**
     * Нужно отрефакторить, простыня
     */
    void Start()
    {
        this.MyTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        this.target = GameObject.FindGameObjectWithTag("Player").transform;

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        //this.HeroAnimator = this.target.gameObject.GetComponent<Animator>();

        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        this.enemyAnimator = GetComponent<Animator>();

        this.enemyAnimator.SetBool("Walk", true);

        //GameObject.rigidbody.freezeRotation = true;
        this.GetComponent<Rigidbody2D>().freezeRotation = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    /**
     * Нужно отрефакторить, простыня кода
     */
    void FixedUpdate()
    {
        if (this.SawHero = this.IsSawEnermy())
        {
            this.isPatrol = false;

            this.Range = Vector2.Distance(this.MyTransform.position, this.target.position);

            if (Range >= 3f)
            {
                if (this.IsGrounded() || !this.IsBlocked())
                {
                    this.Walk();
                }
            }
            else
            {
                //атака или блок
                this.enemyAnimator.ResetTrigger("Walk");

                if (this.target.gameObject.GetComponent<Animator>().GetBool("Attack") && !this.isShield)
                {
                    this.RieseShieldTrigger();
                }

                if (!this.isShield && !this.isAttack)
                {
                    this.StartAttackTrigger();
                }
            }
        }
        else
        {
            this.isPatrol = true;
            this.enemyAnimator.SetBool("Walk", true);
            this.WalkAround();
        }

        if (this.hits <= 0)
        {
            this.Death();
        }
    }

    /**
     * Видим героя или нет
     */
    private bool IsSawEnermy()
    {
        Vector2 SeeLinePosition = this.MyTransform.position.toVector2() - this.MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(SeeLinePosition, SeeLinePosition - MyTransform.right.toVector2() * 16f, Color.green);
        return Physics2D.Linecast(SeeLinePosition, SeeLinePosition - MyTransform.right.toVector2() * 16f, LayerMask.GetMask("Hero"));
    }

    /**
     * Старт анимации щита
     */
    private void RieseShieldTrigger()
    {
        this.isShield = true;
        this.enemyAnimator.SetTrigger("Shild");
        Debug.Log("Up Shield");
    }

    /**
     * Метод-триггер, запускается в конце анимации щита
     */
    private void LowerShieldTrigger()
    {
        this.enemyAnimator.SetBool("Shild", false);
        this.isShield = false;
        Debug.Log("Down Shield");
    }

    /**
     * Старт атаки, запускает анимацию
     */
    private void StartAttackTrigger()
    {
        Debug.Log("Start Attack");
        this.isAttack = true;
        this.enemyAnimator.SetTrigger("Attack");
    }

    /**
     * Метод-триггер, срабатывает по завершению анимации атаки
     */
    private void EndAttackTrigger()
    {
        this.enemyAnimator.SetBool("Attack", false);
        this.isAttack = false;
        Debug.Log("Stop Attack");
    }

    /**
     * Метод запускающий патрулирования
     */
    public void WalkAround()
    {
        this.isAttack = false;
        this.isShield = false;
        this.enemyAnimator.SetBool("Shild", false);
        this.enemyAnimator.SetBool("Attack", false);

        if (this.isPatrol)
        {
            if (!this.IsGrounded() || this.IsBlocked())
            {
                MyTransform.RotateAround(this.MyTransform.position, new Vector3(0, 1, 0), 180f);
            }

            //Always move forward
            this.Walk();
        }
    }

    /**
     * Движение
     */
    private void Walk(float speedMultiplier = 1)
    {
        MyTransform.Translate(Vector3.left * (this.speed * 0.01f) * speedMultiplier);
    }

    /**
     * Метод для рассчета получаемого урона 
     */
    public void TakeDamage(int damage)
    {
        if (!this.isShield)
        {
            this.hits -= damage;
        }
    }

    /**
     * Чардж, вызываем метод движения но с увеличенным множителем скорости
     */
    private void Charge()
    {
        this.Walk(0.5f);
    }

    /**
     * Проверка на препятствие по направлению движения
     */
    private bool IsBlocked()
    {
        Vector2 lineCastPos = MyTransform.position.toVector2() - MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos - MyTransform.right.toVector2(), Color.yellow);

        return Physics2D.Linecast(lineCastPos, lineCastPos - MyTransform.right.toVector2(), enemyMask);
    }

    /**
     * Проверка поверхности для движения
     */
    private bool IsGrounded()
    {
        Vector2 lineCastPos = MyTransform.position.toVector2() - MyTransform.right.toVector2() * myWidth + Vector2.up * myHeight;
        Debug.DrawLine(lineCastPos, lineCastPos + new Vector2(0, -4), Color.red);

        return Physics2D.Linecast(lineCastPos, lineCastPos + new Vector2(0, -4), enemyMask);
    }

    /**
     * Смерть, запускаем анимацию
     */
    public void Death()
    {
        if (isAlive)
        {
            this.enemyAnimator.SetBool("isAlive", false);
            speed = 0;
            isAlive = false;
        }
    }

    /**
     * Тригерим удаление геймобъекта со сцены по окончанию анимации смерти 
     */
    public void DeathTrigger()
    {
        GameObject.DestroyObject(this.gameObject);
    }

    /* void OnTriggerEnter2D(Collider2D col)
     {

         if (col.CompareTag("Player"))
         {

             player.Damag(1);



         }
     }
     */
    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        player.Damag(1);
    //    }
    //}
}
