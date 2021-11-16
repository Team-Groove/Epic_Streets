using UnityEngine;
using System.Collections;

public class EnemyIA : MonoBehaviour
{
    #region VARIABLES


    [SerializeField]private Transform playerSidePos_1;
    [SerializeField]private Transform playerSidePos_2;
    private Animator animator;
    private EnemyMovement movement;
    private EnemyController controller;

    [SerializeField] private float attackDistance;
    [SerializeField] private float timeBetweenAttacks;
    
    private float timer;
    [SerializeField] private float distance1;
    [SerializeField] private float distance2;
    [SerializeField] private float distanceFromObjective;

    public Vector3 pointToGo;
    
    private bool cooling;

    public bool chasingPlayer;
    public bool readyToAttack;
    public bool isAttacking;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerSidePos_1 = GameObject.Find("PointsEnemiesGo1").transform;
        playerSidePos_2 = GameObject.Find("PointsEnemiesGo2").transform;
        movement = GetComponent<EnemyMovement>();
        controller = GetComponent<EnemyController>();
    }
    private void Start()
    {
        timer = timeBetweenAttacks;
    }
    private void Update()
    {
        GetPlayerPos();
        ChooseOnePointToAttack();
        CheckDistanceFromPlayer();
        Attack();
        AttackCooldown();
        DeadAnimation();
        
        Debug.DrawLine(transform.position, pointToGo, Color.yellow);
    }

    #endregion

    #region ANIMATION_EVENTS

    public void isAttackingTrue()
    {
        isAttacking = true;
    }
    public void isAttackingFalse()
    {
        isAttacking = false;
    }
    public void TriggerCooling()
    {
        cooling = true;
    }

    #endregion

    #region PRIVATE_FUNCTIONS

    private void ChasePlayer(bool a)
    {
        if (!controller.isStunned)
        {
            chasingPlayer = a;
            animator.SetBool("canWalk", a);
        }
        
    }
    private void IsReadyToAttack(bool a)
    {
        readyToAttack = a;
        animator.SetBool("Attack", a);
    }
    private void CheckDistanceFromPlayer()
    {
        
        if (distanceFromObjective > attackDistance && !isAttacking && !controller.isStunned)
        {
            ChasePlayer(true);
            IsReadyToAttack(false);
        }
        else if (distanceFromObjective <= attackDistance || distance2 <= attackDistance)
        {
            ChasePlayer(false);
            movement.StopMoving();
            IsReadyToAttack(true);
        }
    }
    private void ChooseOnePointToAttack()
    {
        distance1 = Vector2.Distance(transform.position, playerSidePos_1.transform.position);
        distance2 = Vector2.Distance(transform.position, playerSidePos_2.transform.position);

        if (distance1 > distance2)
        {
            pointToGo = playerSidePos_2.position;
            Debug.Log("Objetivo: Derecha");
        }
        else if (distance1 < distance2)
        {
            pointToGo = playerSidePos_1.position;
            Debug.Log("Objetivo: Izquierda");
        }

        distanceFromObjective = Vector2.Distance(transform.position, pointToGo);

    }
    private void AttackCooldown()
    {
        if (cooling)
        {
            animator.SetBool("Attack", false);
            timer -= Time.deltaTime;

            if (timer <= 0 && cooling && readyToAttack)
            {
                cooling = false;
                timer = timeBetweenAttacks;
            }
        }
    }
    private void Attack()
    {
        if (readyToAttack)
        {
            animator.SetBool("Attack", true);
        }
    }
    private void DeadAnimation()
    {
        if (controller.IsDead)
        {
            animator.SetTrigger("Death");
        }
    }
    
    #endregion

    #region PUBLIC_FUNCTIONS

    public Vector2 GetPlayerPos()
    {
        Vector2 direction = pointToGo - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        return direction;
    }
  
    #endregion
}
