using UnityEngine;
using System.Collections;

public class EnemyController : Fighter
{
    #region OWN_VARIABLES

    private Transform player;
    private EnemyIA ia;
    private SpriteRenderer spriteRenderer;

    public float spriteRendererLocalScaleX;
    public Vector2 playersPos;
    public bool isDummy;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
                Destroy(gameObject);
            }
        }
    }
    private void GetPlayersPos()
    {
        playersPos = player.transform.position;
    }
   
    
    #endregion
}
