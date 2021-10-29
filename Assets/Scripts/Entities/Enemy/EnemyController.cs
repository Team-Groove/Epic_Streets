using UnityEngine;

public class EnemyController : Fighter
{
    #region OWN_VARIABLES

    private Transform player;
    private EnemyIA ia;

    public float spriteRendererLocalScaleX;
    public Vector2 playersPos;
    public bool isDummy;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        

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
