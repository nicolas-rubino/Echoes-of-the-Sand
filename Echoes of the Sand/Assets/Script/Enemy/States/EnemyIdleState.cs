using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class EnemyIdleState : EnemyState
{
  
    public float walkPointRange;

    public EnemyIdleState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
     
    public override void EnterState()
    {
        base.EnterState();

       // _targetPos = GetRandomPointInCircle();
    }


    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
        else{
            enemy.MoveEnemy(Vector3.zero);
        }
        



        //_direction = (_targetPos - enemy.transform.position).normalized;

       // float randomZ = Random.Range();
        
       // enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        /*
        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInCircle();
        }
        */
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.RandomMovementRange;
    }
}
