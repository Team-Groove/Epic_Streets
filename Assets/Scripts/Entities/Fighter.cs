using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    #region VARIABLES
    
    //MOVEMENT
    [SerializeField] public float vertical_speed = 0.0f;
    [SerializeField] public float horizontal_speed = 0.0f;
   
    //STATS
    [SerializeField] public int maxhealth;
    [SerializeField] public int currenthealth;

    //PROTECTEDS
    protected BoxCollider2D boxCollider2D = null;
    public Rigidbody2D rigidBody = null;
    protected SpriteRenderer spriteRenderer = null;
    [SerializeField] public HealthBar healthBar = null;

    //BOOLS
    protected bool IsDead = false;

    #endregion

    #region UNITY_CALLS
    
    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    #endregion

    #region PROTECTED_FUNCTIONS
    protected void UpdateHealthBar(int currenthealth)
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
    
    public IEnumerator RedFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    protected void ResetHealthBar()
    {
        currenthealth = maxhealth;
    }
    public void ReceiveDamage(int dmg)
    {
        currenthealth -= dmg;
        UpdateHealthBar(currenthealth);
    }
    
    #endregion
}