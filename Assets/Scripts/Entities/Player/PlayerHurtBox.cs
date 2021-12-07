using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    #region VARIABLES

    public PlayerAnimation anim;
    private PlayerController player;
    
    public AudioManager audioManager;
    
    [SerializeField]
    private GameObject hitSpawnPoint;

    [SerializeField]
    private ParticleSystem hurtEffect;

    private HealthBarRedFeedback redFeedback;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        anim = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<PlayerController>();
        audioManager = FindObjectOfType<AudioManager>();
        redFeedback = FindObjectOfType<HealthBarRedFeedback>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunch1"))
        {
            anim.StartCoroutine("DamageRedFeedback");
            player.ReceiveDamage(10);

            redFeedback.PlayAnimation();
         


            audioManager.Play("HurtBox_2");

            Instantiate(hurtEffect, new Vector3(
                Random.Range(hitSpawnPoint.transform.position.x + 0.5f, hitSpawnPoint.transform.position.x - 0.5f), 
                Random.Range(hitSpawnPoint.transform.position.y + 0.5f, hitSpawnPoint.transform.position.y - 0.5f), 
                hitSpawnPoint.transform.position.z), Quaternion.identity);

            CineMachineShake.Instance.ShakeCamera(3f, 0.3f);
        }
    }

 
} 

#endregion




