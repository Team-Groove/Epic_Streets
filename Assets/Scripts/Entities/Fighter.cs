    using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    #region VARIABLES
    
    //MOVEMENT
    [SerializeField] public float vertical_speed = 0.0f;
    [SerializeField] public float horizontal_speed = 0.0f;
   
    //STATS
    [SerializeField] public float maxhealth;
    [SerializeField] public float currenthealth;

    //PROTECTEDS
    protected BoxCollider2D boxCollider2D = null;
    public Rigidbody2D rigidBody = null;
   
    [SerializeField] public HealthBar healthBar = null;

    //BOOLS
    public bool IsDead = false;

    #endregion

    #region UNITY_CALLS
    
    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        
    }

    #endregion

    #region PROTECTED_FUNCTIONS
    protected void UpdateHealthBar(float currenthealth)
    {
        if (currenthealth < 0)
        {
            currenthealth = 0;
            healthBar.UpdateHealth(currenthealth);
        }
        else
        {
            healthBar.UpdateHealth(currenthealth);
        }
    }
    
    protected void IfCurrentLifeisZero()
    {
        if (currenthealth <= 0)
        {
            IsDead = true;
        }
        else
        {
            IsDead = false;
        }
    }

    protected void ResetHealthBar()
    {
        currenthealth = maxhealth;
    }
    public void ReceiveDamage(float dmg)
    {
        currenthealth -= dmg;
        UpdateHealthBar(currenthealth);
    }
    
    #endregion
}