using UnityEngine;
using System.Collections;

public class EnemyController : Fighter
{
    #region OWN_VARIABLES

    protected Transform player;
    protected EnemyIA ia;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected SceneController sceneController;
    
    private int randomInt;

    public float stunnedDuration;
    public float spriteRendererLocalScaleX;
    public Vector2 playersPos;

    public bool isStunned;
    public bool isDummy;

    public int moneyReward;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();

        if (sceneController.endGameNum == 1)
        {
            maxhealth *= 1.5f;
        }

        player = FindObjectOfType<PlayerController>().transform;
        playersPos = player.transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxhealth);

        if (!isDummy)
        {
            ia = GetComponent<EnemyIA>();
        }
    }

    private void Update()
    {
        UpdateHealthBar(currenthealth);
        OutOfHealth();
        GetPlayersPos();
        randomInt = Random.Range(0, 1);
    }

    #endregion

    #region FUNCTIONS

    private void OutOfHealth()
    {
        if (currenthealth <= 0)
        {
            if (isDummy)
            {
                currenthealth = maxhealth;
            }
            else
            {
                IsDead = true;
                
            }
        }
    }
    private void GetPlayersPos()
    {
        if (isDummy)
        {
            return;
        }
        else
        {
            if (playersPos == null)
            {
                player = FindObjectOfType<PlayerController>().transform;
            }
            else
            {
                playersPos = player.transform.position;
            }
        }
      
    }
    
    public void AddMoney()
    {
        MoneyController.currentMoney += moneyReward;
    }
    
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public IEnumerator GetStunned()
    {
      
        isStunned = true;

        switch (randomInt)
        {
            case 0:

                animator.Play("Stunned_1");
                
                break;
            
            case 1:

                animator.Play("Stunned_2");
              
                break;
        }

        yield return new WaitForSeconds(stunnedDuration);

        isStunned = false;
    }


    public void EnemyStatusEffectFeedback(Color color)
    {
        spriteRenderer.color = color;
    }
    public void EnemyNoStatusFeedback()
    {
        spriteRenderer.color = Color.white;
    }

   

    #endregion
}
