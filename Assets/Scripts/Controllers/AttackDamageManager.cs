using UnityEngine;

public class AttackDamageManager : MonoBehaviour
{

    public bool canPoison;
    public bool canFreeze;
    public bool canBurn;
    public bool canWindBack;
    public bool canStun;

    public float poisonDuration;
    public float freezeDuration;
    public float burnDuration;
    public float stunDuration;
    public float windForce;

    [SerializeField] public int punch1 = 10;
    [SerializeField] public int punch2 = 15;
    [SerializeField] public int kick1 = 20;
    [SerializeField] public int kick2 = 25;
    [SerializeField] public int final1 = 30;
    [SerializeField] public int longDistance = 10;

}
