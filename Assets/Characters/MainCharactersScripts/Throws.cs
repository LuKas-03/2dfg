using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Throws : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Vector2 direction;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    // получение направления, куда полетит объект
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    // исчезновение объекта за пределами просмотра (чтобы не возникало кучи копий на сцене
    // и не начиналась просадка фпс)
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // исчезновение стрел (и других летящих объектов) при попадании в персонажа
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Character" && FindObjectOfType<Enemy>().attack)
        {
            Destroy(gameObject);
        }
    }
}
