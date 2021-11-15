using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region VARIABLES

    //VARIABLE TOMA DEL INPUTCONTROLLER
    private Vector2 controlAxis;
    private Dash dash;
    private AttackSystem attack;
    private PlayerAnimation anim;
    private PlayerController player;

    #endregion

    #region BOOLS

    //PUBLIC 

    public bool isMoving;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        OnGetComponentStart();
    }

    private void Update()
    {
        FlipSprite();
        CheckIfMoving();
    }

    private void FixedUpdate()
    {
        VelocityMovement(new Vector2(player.horizontal_speed, player.vertical_speed), controlAxis);
        StopMovementY();
        StopWhenDead();
    }

    #endregion

    #region PRIVATE_FUNCTIONS
    
    private void VelocityMovement(Vector2 movementSpeed, Vector2 direction)
    {
        if (!dash.isDashing && !attack.isAttacking && anim.canMove)
        {
            player.rigidBody.velocity = new Vector2(direction.x * movementSpeed.x, direction.y * movementSpeed.y);
        }
    }
    private void FlipSprite()
    {
        if (player.IsDead)
        {
            return;
        }
        else
        {
            if (controlAxis.x > 0 && anim.canMove)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (controlAxis.x < 0 && anim.canMove)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
    }
    private void CheckIfMoving()
    {
        if (controlAxis != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    private void StopMovementY()
    {
        if (player.rigidBody.velocity.y != 0  && attack.isAttacking)
        {
            player.rigidBody.velocity = Vector2.zero;
        }
    }
    
    private void StopWhenDead()
    {
        if (player.IsDead)
        {
            player.rigidBody.velocity = Vector2.zero;
        }
    }
    private void OnGetComponentStart()
    {
        dash = GetComponent<Dash>();
        attack = GetComponent<AttackSystem>();
        anim = GetComponent<PlayerAnimation>();
        player = GetComponent<PlayerController>();
    }

    #endregion

    #region PUBLIC_FUNCTION

    //NO SE LLAMA EN UPDATE
    public void ReadInputMove(InputAction.CallbackContext context)
    {
        controlAxis.x = context.ReadValue<Vector2>().x;
        controlAxis.y = context.ReadValue<Vector2>().y;
    }

    #endregion
}
