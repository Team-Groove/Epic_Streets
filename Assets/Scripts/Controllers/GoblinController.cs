using UnityEngine;
public class GoblinController : EnemyController
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject spawnPoint;
    public void DistanceAttack()
    {
        Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        playersPos = player.transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxhealth);

        if (!isDummy)
        {
            ia = GetComponent<EnemyIA>();
        }

        animator.Play("Entrance");
    }
}
