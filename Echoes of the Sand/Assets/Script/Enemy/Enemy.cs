using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [field: SerializeField] public float MaxHealth { get; set;} = 100f;

    Animator animatorController;
    public float CurrHealth { get; set; }
    public Rigidbody RB { get; set; }
    public float rotationY { get; set; }
    [SerializeField] public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }

    // STATE MACHINE VARIABLES
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set ;}
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }


    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    private void Start(){
        CurrHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();
        animatorController = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();

        
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }
    public void Damage(float damagePoints)
    {
        CurrHealth =- damagePoints;
        
        if (CurrHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this);
    }

    public void MoveEnemy(Vector3 velocity)
    {
        RB.velocity = velocity;
        Check();
    }

    public void Check()
    {
       // throw new System.NotImplementedException();
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        // TODO fill in one state machine is create
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
        animatorController.SetBool("IsAggroed",isAggroed);
        Debug.Log("AGGRO distance ACTIVATED");

    }

    public void SetStrikingDistance(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
        animatorController.SetBool("IsWithinStrikingDistance",isWithinStrikingDistance);
        Debug.Log("Striking distance ACTIVATED");

    }

    public void Attack()
    {

    }
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    
}
