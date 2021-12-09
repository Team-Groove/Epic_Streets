using UnityEngine;

public class PlayerHurtBox : MonoBehaviour
{
    #region VARIABLES

    private PlayerAnimation anim;
    private PlayerController player;
    private AttackDamageManager damageManager;
    private AudioManager audioManager;
    private HealthBarRedFeedback redFeedback;

    public bool canBeDmg;
    public float invulnerabilityTime = 0.5f;
    private float inviTimer;

    [SerializeField] private GameObject hitSpawnPoint;
    [SerializeField] private ParticleSystem hurtEffect;
    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        anim = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<PlayerController>();
        audioManager = FindObjectOfType<AudioManager>();
        redFeedback = FindObjectOfType<HealthBarRedFeedback>();
        damageManager = FindObjectOfType<AttackDamageManager>();

        inviTimer = invulnerabilityTime;
        canBeDmg = true;
    
    }

    private void Update()
    {
        CanBeDmgTimer();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttack(collision, "EnemyPunch1", damageManager.fighterDmg);
        CheckAttack(collision, "EnemyProjectile", damageManager.goblinDmg);
        CheckAttack(collision, "BossAttack", damageManager.bossDmg);
    }

    private void CheckAttack(Collider2D collision, string tag, float dmg)
    {
        if (collision.gameObject.CompareTag(tag) && canBeDmg)
        {
            
            anim.StartCoroutine("DamageRedFeedback");
            player.ReceiveDamage(dmg);

            redFeedback.PlayAnimation();

            audioManager.Play("HurtBox_2");

            Instantiate(hurtEffect, new Vector3(
                Random.Range(hitSpawnPoint.transform.position.x + 0.5f, hitSpawnPoint.transform.position.x - 0.5f),
                Random.Range(hitSpawnPoint.transform.position.y + 0.5f, hitSpawnPoint.transform.position.y - 0.5f),
                hitSpawnPoint.transform.position.z), Quaternion.identity);

            CineMachineShake.Instance.ShakeCamera(3f, 0.3f);

            canBeDmg = false;
        }
    }
    private void CanBeDmgTimer()
    {
        if (!canBeDmg)
        {
            if (inviTimer > 0)
            {
                inviTimer -= Time.deltaTime;
            }
            else
            {
                inviTimer = invulnerabilityTime;
                canBeDmg = true;
            }
        }
    }
} 

#endregion




