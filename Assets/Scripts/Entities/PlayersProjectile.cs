using UnityEngine;

public class PlayersProjectile : MonoBehaviour
{
    #region VARIABLES

    private PlayerMovement player;
    private DistanceAttackSystem shootingSystem;
    
    private int direction;

    public float timeBeforeDestroy;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        CheckDirection();
        TimeBeforeDestroy();
    }

    #endregion

    #region FUNCTIONS
    private void OnAwake()
    {
        player = FindObjectOfType<PlayerMovement>();
        shootingSystem = FindObjectOfType<DistanceAttackSystem>();
        direction = 0;
    }
    private void OnStart()
    {
        if (player.transform.localScale.x > 0)
        {
            direction = 2;
        }
        else if (player.transform.localScale.x <= 0)
        {
            direction = 1;
        }

        timeBeforeDestroy = shootingSystem.timeBeforeDestroy;

    }
    private void CheckDirection()
    {
        if (direction == 1)
        {
            transform.Translate(Vector2.left * shootingSystem.projectileSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * shootingSystem.projectileSpeed * Time.deltaTime);
        }
    }
    private void TimeBeforeDestroy()
    {
        if (timeBeforeDestroy > 0)
        {
            timeBeforeDestroy -= Time.deltaTime;
        }
        else if (timeBeforeDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region ONTRIGGER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    
    #endregion

}
