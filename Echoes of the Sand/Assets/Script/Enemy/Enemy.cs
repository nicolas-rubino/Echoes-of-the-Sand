using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [field: SerializeField] public float MaxHealth { get; set;} = 100f;

    [SerializeField] Animator animatorController;
    public float CurrHealth { get; set; }
    public Rigidbody RB { get; set; }
    public float rotationY { get; set; }
    [SerializeField] public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    public bool IsAttackOnCooldown { get; set; }

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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            Damage(50);
            Destroy(collision.gameObject);
        }
    }
    public void Damage(float damagePoints)
    {
        CurrHealth =- damagePoints;
        
        if (CurrHealth <= 0f)
        {
            animatorController.Play("Death");
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
        //animatorController.SetBool("IsWithinStrikingDistance",isWithinStrikingDistance);
        Debug.Log("Striking distance ACTIVATED");

    }

    public void SetAttackOnCooldown(bool isAttackOnCooldown)
    {
        IsAttackOnCooldown = isAttackOnCooldown;
        animatorController.SetBool("IsAttackOnCooldown", isAttackOnCooldown);
        Debug.Log("Cooldown ACTIVATED");

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

