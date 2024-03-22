using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;

    private float _timer;

    private float _exitTimer;
    private float _timerTillExit;

    private float _timeBetweenShots = 2f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine fsm) : base(enemy, fsm)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.GetComponent<Animator>().Play("attack");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().TakeDamage();
    }

    public override void ExitState()
    {
        base.ExitState();
    }


    public override void FrameUpdate()
    {
        base.FrameUpdate();

        
        enemy.MoveEnemy(Vector3.zero);
        if ( _timer > _timeBetweenShots)
        {
            _timer = 0f;

            enemy.GetComponent<Animator>().Play("attack");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().TakeDamage();

        }

        if (Vector3.Distance(_playerTransform.position, enemy.transform.position) > 5)
        {
            _exitTimer += Time.deltaTime;
            
            if(_exitTimer > _timerTillExit)
            {
                Debug.Log("CHANGED TO CHASE");
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
            
        }
        _timer += Time.deltaTime;
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
