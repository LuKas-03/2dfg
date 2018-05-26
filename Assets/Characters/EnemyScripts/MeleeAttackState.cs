using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : IEnemyStates {

    private Enemy enemy;

    // переменные для ближней атаки (меч/нож/топор и т.д.)
    private float meleeRangeAttackTimer;
    private float meleeRangeAttackCoolDown = 4f;
    private bool canAttack = true;


    // точка входа в состояние
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // Update (обновляет состояния)
    public void Execute()
    {
        MeleeRangeAttack();
        if (enemy.isInThrowRange && !enemy.isInMeleeRange)
        {
            enemy.ChangeState(new TargetInRangeState());
        }
        else if (enemy.Target == null)
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

    // ближняя атака (меч/нож/топор и т.д.)
    private void MeleeRangeAttack()
    {
        meleeRangeAttackTimer += Time.deltaTime;
        if (meleeRangeAttackTimer >= meleeRangeAttackCoolDown)
        {
            canAttack = true;
            meleeRangeAttackCoolDown = 0;
        }
        if (canAttack)
        {
            canAttack = false;
            Enemy.anim.SetTrigger("Attack");
        }
    }
}
