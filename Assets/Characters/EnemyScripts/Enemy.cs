using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private IEnemyStates currentState;

    /// цель для врага
    public GameObject Target { get; set; }

    /// переменные для передвижения
    public float moveSpeed = 10f;
    private bool isFacingRight = true;
    public static Animator anim;

    /// переменные для дальней атаки
    public bool attack { get; set; }
    [SerializeField]
    private Transform throwsPosition;
    [SerializeField]
    private GameObject throwsPrefab;
    [SerializeField]
    private float throwRange;
    public bool isInThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;
        }
    }

    /// переменные для ближней атаки
    [SerializeField]
    private float meleeRange;
    public bool isInMeleeRange
    {
        get
        {
            if(Target!=null)
            {
                return Vector2.Distance(transform.position,Target.transform.position)<= meleeRange;
            }
            return false;
        }
    }

    /// переменные, отвечающие за здоровье и наносимый врагу урон
    [SerializeField]
    protected float health;
    [SerializeField]
    private List<string> canTakeDamageFrom;
    public bool isTakingDamage { get; set; }
    public bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    int count = 1;

    /// переменная для генерации лута/денег
    private bool isGenerated = false;

    /// Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        ChangeState(new IdleState());
        FindObjectOfType<CharacterAnimationController>().Dead +=new TargetIsDead(RemoveTarget);
    }
	
	/// Update is called once per frame
	void Update ()
    {
        if (!isDead)
        {
            if (!isTakingDamage)
            {
                currentState.Execute();
            }
            TurnToTarget();
        }
    }

    /// смена состояний
    public void ChangeState(IEnemyStates newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    /// передвижение (для патрулирования)
    public void Move()
    {
        if (!attack)
        {
            Enemy.anim.SetFloat("Speed", 1);
            /// умножение на deltaTime нужно, чтобы не возникало быстрого вычисления на мощных пк или
            /// медленного на слабых (в противном случае на разных пк скорость AI будет разной)
            transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
        }
    }

    /// проверка на то, куда повернуто лицо
    public Vector2 GetDirection()
    {
        return isFacingRight ? Vector2.right : Vector2.left;
    }

    /// поворот на 180
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// поворот к цели
    private void TurnToTarget()
    {
        if (Target != null)
        {
            float direction = Target.transform.position.x - transform.position.x;
            if (direction < 0 && isFacingRight || direction > 0 && !isFacingRight)
            {
                Flip();
            }
        }
    }

    /// потеря цели
    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    /// использование триггеров в состояниях
    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    /// бросание объекта (стрелы у лука/ножи и т.д.)
    public void ThrowObject()
    {
        if (isFacingRight)
        {
            GameObject tmp = (GameObject)Instantiate(throwsPrefab, throwsPosition.position, Quaternion.identity);
            tmp.GetComponent<Throws>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(throwsPrefab, throwsPosition.position, Quaternion.Euler(0, 180, 0));
            tmp.GetComponent<Throws>().Initialize(Vector2.left);
        }
    }

    /// получение урона
    public IEnumerator Damage()
    {
        health -= FindObjectOfType<UpdateSystem>().FunctionldamageFiz();
        if (!isDead)
        {
            Enemy.anim.SetTrigger("Hit");
        }
        else
        {
            Enemy.anim.SetTrigger("Death");           
            if (count == 1)
            {
                FindObjectOfType<UpdateSystem>().FunctionPoint(50f);
            }
            count = 0;
        }
            yield return null;
     }

    /// соприкосновение с оружием врага (таким образом получается урон)
    public void OnTriggerStay2D(Collider2D other)
    {
        if (canTakeDamageFrom.Contains(other.tag) && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Damage());
        }
        if (isDead&& !isGenerated)
        {
            LootGeneration();
        }
    }

   /// генерация золота из трупа (в будущем ещё и лута)
    private void LootGeneration()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Character")[0];

        int x = Physics2D.GetContacts(gameObject.GetComponent<Collider2D>(), new Collider2D[] { player.GetComponent<Collider2D>() });
        if (Input.GetKeyDown(KeyCode.E) && x > 0)
        {
            isGenerated = true;
            var value = Random.Range(10, 100);
            var inventory = FindObjectOfType<Inventory>();
            inventory.money += value;

            FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice("Нашёл деньги", "+ " + value + " монет"));

            Debug.Log(inventory.money);
        }
    }
}