using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRangeState : IEnemyStates {

    private Enemy enemy;

    // переменные для дальней атаки врага (лук, ножи для метания и т.д.)
    private float throwRangeAttackTimer;
    private float throwRangeAttackCoolDown=2f;
    private bool  canShot=true;

    // точка входа в состояние
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // Update (обновляет состояния)
    public void Execute()
    {
        ThrowRangeAttack();
        if (enemy.isInMeleeRange)
        {
            enemy.ChangeState(new MeleeAttackState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    // точка выхода из состояния
    public void Exit()
    {

    } 


    public void OnTriggerEnter(Collider2D other)
    {

    }

    // дальняя атака (лук, ножи для метания и т.д.)
    private void ThrowRangeAttack()
    {
        throwRangeAttackTimer += Time.deltaTime;
        if (throwRangeAttackTimer >= throwRangeAttackCoolDown)
        {
            canShot = true;
            throwRangeAttackCoolDown = 0;
        }
        if (canShot)
        {
            canShot = false;
            Enemy.anim.SetTrigger("Shot");
        }
    }
}
