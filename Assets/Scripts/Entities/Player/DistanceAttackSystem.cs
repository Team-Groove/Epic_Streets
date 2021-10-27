using UnityEngine;
using UnityEngine.InputSystem;

public class DistanceAttackSystem : MonoBehaviour
{
    #region PRIVATE_VARIABLES

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform instanciatePoint;
    [SerializeField] private float timeCooldown;
    
    private bool canShoot;
    private float shootingCooldown;

    #endregion

    #region PUBLIC_VARIABLES
    
    public bool distanceAttack;
    public float timeBeforeDestroy;
    public float projectileSpeed;

    #endregion

    #region UNITY_CALLS
    
    private void Start()
    {
        shootingCooldown = timeCooldown;
        canShoot = true;
    }

    void Update()
    {
        ShootingCooldown();
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
    private void ShootingCooldown()
    {
        if (shootingCooldown > 0 && !canShoot)
        {
            shootingCooldown -= Time.deltaTime;
        }
        else if (shootingCooldown <= 0)
        {
            canShoot = true;
            shootingCooldown = timeCooldown;
            return;
        }
    }

    #endregion
}
