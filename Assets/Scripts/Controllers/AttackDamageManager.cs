using UnityEngine;

public class AttackDamageManager : MonoBehaviour
{

    private SceneController sceneController;
    private int multiplier;
    
    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();

        if (sceneController.endGameNum > 0)
        {
            multiplier = sceneController.endGameNum + 1;

            fighterDmg *= multiplier / 1.5f;
            goblinDmg *= multiplier / 1.5f;
            bossDmg *= multiplier / 1.5f;
        }
    }

    private void Update()
    {
       
    }

    [HideInInspector] public bool canPoison;
    [HideInInspector] public bool canFreeze;
    [HideInInspector] public bool canBurn;
    [HideInInspector] public bool canWindBack;
    [HideInInspector] public bool canStun;
    [HideInInspector] public bool criticalHit;

    [Header("Poison Stats")]
    public float poisonTotalDamage;
    public float poisonDamagePerLoop;
    public float poisonInterval;

    [Header("Freeze Stats")]
    public float freezeDuration;
    public float slowAmount;

    [Header("Burning Stats")]
    public float burnTotalDamage;
    public float burnDamagePerLoop;
    public float burnInterval;
    
    public float stunDuration;
    public float windForce;

    [Header("Player Attack Stats")]
    [SerializeField] public float punch1 = 10;
    [SerializeField] public float punch2 = 15;
    [SerializeField] public float kick1 = 20;
    [SerializeField] public float kick2 = 25;
    [SerializeField] public float final1 = 30;
    [SerializeField] public float longDistance = 10;

    [Header("Enemy Stats")]
    [SerializeField] public float fighterDmg;
    [SerializeField] public float goblinDmg;
    [SerializeField] public float bossDmg;

}
