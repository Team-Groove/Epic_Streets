using UnityEngine;
using System.Collections;

public class EnemyController : Fighter
{
    #region OWN_VARIABLES

    private Transform player;
    private EnemyIA ia;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float stunnedDuration;
    public float spriteRendererLocalScaleX;
    public Vector2 playersPos;

    public bool isStunned;
    public bool isDummy;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
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
        playersPos = player.transform.position;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public IEnumerator GetStunned()
    {
        int random;
        random = Random.Range(0, 1);

        isStunned = true;

        switch (random)
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

    public IEnumerator EnemyDamageRedFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    #endregion
}
