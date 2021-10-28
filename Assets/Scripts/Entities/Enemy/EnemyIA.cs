using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    #region VARIABLES
    
  
    private Transform player;
    private Animator animator;
    private EnemyController enemy;

   
    public float attackDistance;

    public float cooldownTimer;
    public float intTimer;
    private Vector2 movement;
    public float distance;

    public bool cooling;

    public bool followPlayer;
    public bool chasingPlayer;
    public bool attackMode;
    public bool isAttacking;

    #endregion

    private void Start()
    {
       
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>().transform;

        
        followPlayer = true;
        attackDistance = 2f;
        intTimer = 2f;


    }
    private void Update()
    {

        //DESIGN TOOL VARIABLES

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > attackDistance && !isAttacking)
        {

            chasingPlayer = true;
            animator.SetBool("canWalk", true);

        }
        else if (distance < attackDistance)
        {
            chasingPlayer = false;
            animator.SetBool("canWalk", false);
            if (!isAttacking)
            {
            
                attackMode = true;
        
            }
            else
            {
                attackMode = false;
            }
        }

        if (attackMode)
        {
            Attack();
        }

        if (cooling)
        {
            AttackCooldown();
            animator.SetBool("Attack", false);
        }
        GetPlayerPos();
        if (!followPlayer)
        {
            StopMoving();
        }


    }

    private void FixedUpdate()
    {
        if (followPlayer)
        {
            if (chasingPlayer && !isAttacking)
            {
                moveCharacter(movement);
            }
            else 
            {
                StopMoving();
            }

        }
    }

    private void moveCharacter(Vector2 direction)
    {
        //rigidBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void GetPlayerPos()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }
    private void StopMoving()
    {
        //rigidBody.velocity = Vector2.zero;
        animator.SetBool("canWalk", false);
    }

    public void isAttackingTrue()
    {
        isAttacking = true;
    }
    public void isAttackingFalse()
    {
        isAttacking = false;
    }
    private void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void StopAttacking()
    {
        attackMode = false;
        animator.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private void AttackCooldown()
    {

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0 && cooling && attackMode)
        {
            cooling = false;
            cooldownTimer = intTimer;
        }

    }

}
