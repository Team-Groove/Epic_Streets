using UnityEngine;
using UnityEngine.InputSystem;

public class DistanceAttackSystem : MonoBehaviour
{
    #region PRIVATE_VARIABLES

    private AttackSystem attackSystem;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform instanciatePoint;
    
    public bool canShoot;
    
    #endregion

    #region PUBLIC_VARIABLES
    
    public bool distanceAttack;
    public float timeBeforeDestroy;
    public float projectileSpeed;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        attackSystem = GetComponent<AttackSystem>();
    }

    private void Start()
    {
        canShoot = true;
    }

    #endregion

    #region FUNCTIONS
    
    public void DistanceAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot)
        {
            distanceAttack = true;
            canShoot = false;
        }
        else
        {
            distanceAttack = false;
        }
    }
    public void Shoot()
    {
        Instantiate(projectile, instanciatePoint.position, Quaternion.identity); 
    }


    #endregion
}
