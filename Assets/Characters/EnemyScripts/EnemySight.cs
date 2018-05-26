using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;

    public Transform sightStart, sightEnd;
    public bool spotted = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Raycasting();
        Behaviours();
    }

    // рисование луча и определение цели с помощью слоя, на котором она должна быть
    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

    }

    // поведение ИИ
    void Behaviours()
    {
        // нахождение цели, если она в зоне видимости
        if (spotted)
        {
            enemy.Target = FindObjectOfType<CharacterAnimationController>().gameObject;
        }
        // потеря цели из виду
        else
        {
            enemy.Target = null;
        }

    }
}