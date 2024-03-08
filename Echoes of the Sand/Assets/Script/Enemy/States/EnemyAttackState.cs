using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _timer;
    private float _attackCooldown = 2f;
    private float _exitTimer;
    private float _timeTillExit = 3f;
    private float _timeBetweenShots = 3f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }


    public override void FrameUpdate()
    {
        base.FrameUpdate();

        
        enemy.MoveEnemy(Vector3.zero);


        if(enemy.IsWithinStrikingDistance == false && enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
        if(enemy.IsAggroed == false){
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        /*
        if(_timer > _timeBetweenShots)
        {
            _timer = 0f;

            
        }
        _timer += Time.deltaTime;
        */

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
