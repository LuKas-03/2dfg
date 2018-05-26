using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyStates {

    private Enemy enemy;

    private float idleTimer;
    private float idleDuration=5f;

    // точка входа в состояние
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // Update (обновляет состояния)
    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    // точка выхода из состояния
    public void Exit() { } 


    public void OnTriggerEnter(Collider2D other) { }

    // переход от остановки к патрулированию спустя фиксированное время 
    // (по желанию можно сделать рандом в небольших пределах)
    private void Idle()
    {
        Enemy.anim.SetFloat("Speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
