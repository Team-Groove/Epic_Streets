using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossController : MonoBehaviour
{
    [SerializeField] private GameObject enemieSpawnPoint_1 = null;
    [SerializeField] private GameObject enemieSpawnPoint_2 = null;

    [SerializeField] private GameObject goblinPrefab = null;
    
    [SerializeField] private EnemyController boss = null;
    
    [SerializeField] private GoblinController minionSlot_1 = null;
    [SerializeField] private GoblinController minionSlot_2 = null;

    public float spawnTimer;
    public float spawnTimer_1;
    public bool canSpawn;
    public bool canSpawn_1;
    
    public float timeBetweenSpawn;

  
    private void Start()
    {
        spawnTimer = timeBetweenSpawn;
        spawnTimer_1 = timeBetweenSpawn;
    }
    
    private void Update()
    {
        CheckIfMinionsDeployed();
        SpawnEnemy();
        ResetSlot();

        CheckIfMinionsDeployed_1();
        SpawnEnemy_1();
        ResetSlot_1();

    }
    private void CheckIfMinionsDeployed()
    {

        if (!canSpawn && minionSlot_1 == null)
        {
            if (spawnTimer > 0)
                spawnTimer -= Time.deltaTime;

            else
            {
                canSpawn = true;
                spawnTimer = timeBetweenSpawn;

            }

        }

    }
    private void SpawnEnemy()
    {
        if (canSpawn)
        {
            minionSlot_1 = Instantiate(goblinPrefab, enemieSpawnPoint_1.transform.position, Quaternion.identity).GetComponent<GoblinController>();
            canSpawn = false;
        }
    }
    private void ResetSlot()
    {
        if (minionSlot_1 != null)
        {
            if (minionSlot_1.IsDead)
                minionSlot_1 = null;
        }
        else return;

    }
    private void CheckIfMinionsDeployed_1()
    {

        if (!canSpawn_1 && minionSlot_2 == null)
        {
            if (spawnTimer_1 > 0)
                spawnTimer_1 -= Time.deltaTime;

            else
            {
                canSpawn_1 = true;
                spawnTimer_1 = timeBetweenSpawn;

            }
        }
       
    }
    private void SpawnEnemy_1()
    {
        if (canSpawn_1)
        {
            minionSlot_2 = Instantiate(goblinPrefab, enemieSpawnPoint_2.transform.position, Quaternion.identity).GetComponent<GoblinController>();
            canSpawn_1 = false;
        }
    }
    private void ResetSlot_1()
    {
        if (minionSlot_2 != null)
        {
            if (minionSlot_2.IsDead)
                minionSlot_2 = null;
        }
        else return;
       
    }

 
}
