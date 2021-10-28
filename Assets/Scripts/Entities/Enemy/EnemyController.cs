using UnityEngine;

public class EnemyController : Fighter
{
    #region OWN_VARIABLES

    private Transform player;
    public bool isDummy;

    [SerializeField] EnemyIA ia;

    //PARA HACER EL SPRITE FLIP
    private float spriteRendererLocalScaleX;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        spriteRendererLocalScaleX = spriteRenderer.transform.localScale.x;

        if (!isDummy)
        {
            ia = GetComponent<EnemyIA>();
        }

    }

    private void Update()
    {
        UpdateHealthBar(currenthealth);
        FlipSprite();
        OutOfHealth();
    }

    #endregion

    #region FUNCTIONS

    private void FlipSprite()
    {
        if (!isDummy)
        {
            if (!ia.isAttacking)
            {
                if (player.position.x > transform.position.x)
                {
                    spriteRenderer.transform.localScale = new Vector3(spriteRendererLocalScaleX, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
                else if (player.position.x < transform.position.x)
                {
                    spriteRenderer.transform.localScale = new Vector3(-spriteRendererLocalScaleX, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
            }
        }
    }
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
                Destroy(gameObject);
            }
        }
    }

    #endregion
}
