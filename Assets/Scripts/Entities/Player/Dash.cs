using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Dash : MonoBehaviour
{

    #region FLOAT_VARIABLES

    [SerializeField] private float dashSpeed;
    [SerializeField] private float startDashTime;

    private float dashTime;

    //DIRECCION EN INT

    public int direction;

    //COOLDOWN

    [SerializeField] private float dashCooldown;
    private float dashTimeCooldown;
    
    #endregion

    #region BOOL_VARIABLES

    //STATUS
    private bool canDash;
    public bool isDashingY;
    public bool isDashingX;

    //FUNCIONALIDAD
    private bool dashStop;
    private bool dashRight;
    private bool dashLeft;
    private bool dashUp;
    private bool dashDown;

    //COLOR
    private bool visualDashFeedback;
    private bool turnToNormalColor;

    #endregion

    #region OTHER_VARIABLES

    [SerializeField] private Color visualDashFeedbackColor;
   
    private Rigidbody2D rigidBody;
    private PlayerInput input;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;
    [SerializeField] private GameObject deactiavateBoxCollider;
    
    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        canDash = true;
        dashTime = startDashTime;
        controller = GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        DashState();
        CooldownColor();
        DeactivateDashDuring();
    }

    private void FixedUpdate()
    {
        DashMovementPhysics();
    }

    #endregion

    #region FUNCTIONS

    public void DashInput(InputAction.CallbackContext context)
    {
        if (canDash && !controller.InDialogue())
        {
            if (context.performed && input.actions.FindAction("Move").ReadValue<Vector2>().x > 0.1f)
            {
                direction = 1;
            }
            else if (context.performed && input.actions.FindAction("Move").ReadValue<Vector2>().x < -0.1f)
            {
                direction = 2;
            }
            else if (context.performed && input.actions.FindAction("Move").ReadValue<Vector2>().y > 0.1f)
            {
                direction = 3;
            }
            else if (context.performed && input.actions.FindAction("Move").ReadValue<Vector2>().y < -0.1f)
            {
                direction = 4;
            }
        }
    }
    private void DashState()
    {
        
        //DASH EN STOP ES IGUAL A 0
        if (direction == 0)
        {
            StopDashes();
            DashCooldown();
        }
        else
        {
            //SI DASHTIME ES IGUAL O MAYOR A CERO COOLDOWN SE REINICIA
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                dashStop = true;
                dashTimeCooldown = dashCooldown;
            }
            else
            {
                dashTime -= Time.deltaTime;
                canDash = false;

                SwitchDashesDirections();
                CheckIfDashin();
            }
        }
        
    }
    private void DashCooldown()
    {
       
        if (dashTimeCooldown > 0)
        {
            dashTimeCooldown -= Time.deltaTime;
        }
        else if (dashTimeCooldown <= 0)
        {
            canDash = true;
        }
        
    }
    private void DashMovementPhysics()
    {
        if (dashRight)
        {
            rigidBody.velocity = Vector2.right * dashSpeed;
        }
        else if (dashLeft)
        {
            rigidBody.velocity = Vector2.left * dashSpeed;
        }
        else if (dashUp)
        {
            rigidBody.velocity = Vector2.up * dashSpeed / 2;
        }
        else if (dashDown)
        {
            rigidBody.velocity = Vector2.down * dashSpeed / 2;
        }
        else if (dashStop)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
    private void CheckIfDashin()
    {
        if (direction == 1 || direction == 2)
        {
            isDashingX = true;
        }
        else if (direction == 3 || direction == 4)
        {
            isDashingY = true;
        }
        else if(direction == 0)
        {
            isDashingX = false;
            isDashingY = false;
        }
    }
        
    private void DeactivateDashDuring()
    {
        if (isDashingX || isDashingY)
        {
            deactiavateBoxCollider.SetActive(false);
        }
        else
        {
            deactiavateBoxCollider.SetActive(true);
        }
    }

    private void StopDashes()
    {
        isDashingY = false;
        isDashingX = false;
        dashRight = false;
        dashLeft = false;
        dashStop = false;
        dashUp = false;
        dashDown = false;
    }
    private void SwitchDashesDirections()
    {
        switch (direction)
        {
            case 1:
                
                dashRight = true;
                break;
            case 2:
                dashLeft = true;
                break;
            case 3:
                dashUp = true;
                break;
            case 4:
                dashDown = true;
                break;
        }
    }
    private void CooldownColor()
    {
        if (direction != 0)
        {
            StartCoroutine(ColorCooldown());
        }
    }
    private IEnumerator ColorCooldown()
    {
        spriteRenderer.color = visualDashFeedbackColor;
      
        yield return new WaitForSeconds(dashCooldown);

        spriteRenderer.color = Color.white;
    }

    #endregion
}
