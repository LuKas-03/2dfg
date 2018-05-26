using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void TargetIsDead();

public class CharacterAnimationController : MonoBehaviour {

    // переменые для передвижения
    public float moveSpeed = 10f;
    private bool isFacingRight = true;
    public static Animator anim;

    // переменные для прыжка
    private bool isGrounded;
    public Vector2 jumpHeight;

   // public Image healthbar;
   // float maxHealth; 

    // самая главная часть для управления анимациями - получение анииматора
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = FindObjectOfType<UpdateSystem>().FunctionHels();
        //maxHealth = FindObjectOfType<UpdateSystem>().FunctionHels();
    }

    // Update (обновления состояний: ходьба, прыжок, атака)
    void Update()
    {
        ///healthbar.fillAmount = health / maxHealth; //health;
        
        if (!isDead)
        {
            if (!isTakingDamage)
            {
                if (anim.GetBool("StopMovement") == false)
                {
                    Walk();
                    if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                    {
                        Jump();
                        isGrounded = false;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                    }
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        FindObjectOfType<OknoSkills>().StartSkills();
                        anim.SetBool("StopMovement", true);
                    }
                }
            }
        }
    }

    // ходьба с поворотами на 180
    public void Walk()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // прыжок
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(jumpHeight, ForceMode2D.Impulse);
        anim.SetBool("Jump", true);
    }

    // атака
    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    // поворот на 180
    void Flip()
    {
         isFacingRight = !isFacingRight;
         Vector3 theScale = transform.localScale;
         theScale.x *= -1;
         transform.localScale = theScale;
    }

    // GroundCheck (проверка на нахождение на земле, НЕОБХОДИМО для нормального прыжка, 
    // иначе можно бесконечно прыгать вверх и улететь со сцены)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }         
    }

    //////////////////////////////////////////////////////////////

    // переменные, отвечающие за здоровье, наносимый персонажу урон и смерть
    [SerializeField]
    protected float health;//=FindObjectOfType<UpdateSystem>().FunctionHels();
    [SerializeField]
    private List<string> canTakeDamageFrom;
    public bool isTakingDamage { get; set; }
    public bool isDead
    {
        get
        {
            if (health <= 0)
            {
                IsDead();
            }
            return health <= 0;
        }
    }

   public event TargetIsDead Dead;

    // получение урона
    public IEnumerator Damage()
    {
        health -= FindObjectOfType<UpdateSystem>().FunctionDamege();
        if (!isDead)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            anim.SetTrigger("Death");
            yield return null;
        }
    }

    // соприкосновение с оружием врага (таким образом получается урон)
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamageFrom.Contains(other.tag) && FindObjectOfType<Enemy>().attack)
        {
            StartCoroutine(Damage());
        }
    }

    // триггер для фиксации смерти игрока (нужно для передачи этих данных врагу, 
    // чтобы он перестал бить труп :D)
    public void IsDead()
    {
        if (Dead != null)
        {
            Dead();
            anim.SetTrigger("Death");
        }
    }

}
