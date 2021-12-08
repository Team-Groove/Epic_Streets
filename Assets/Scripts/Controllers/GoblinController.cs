using UnityEngine;
public class GoblinController : EnemyController
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject spawnPoint;
    public void DistanceAttack()
    {
        Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
    }
}
