using System.Collections;
using UnityEngine;


public enum _statusEffects {normal, poisoned, freezed, burned, stunned, pushedBack }

public class StatusEffects : MonoBehaviour
{
    private EnemyMovement movement;
    private EnemyHurtBox hurtbox;
    private EnemyIA ia;
    private AttackDamageManager damageManager;
    private EnemyController controller;

    public _statusEffects actualStatusEffect;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        hurtbox = GetComponent<EnemyHurtBox>();
        ia = GetComponent<EnemyIA>();
        controller = GetComponent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
    }

    private void Update()
    {
        StatusEffectsProperties();
    }

    private void StatusEffectsProperties()
    {
        switch (actualStatusEffect)
        {
            case _statusEffects.normal:
                
                

                break;
            
            case _statusEffects.poisoned:

                StartCoroutine(WhileStatIsOn(damageManager.poisonDuration, Color.green));

                break;
            
            case _statusEffects.freezed:

                StartCoroutine(WhileStatIsOn(damageManager.freezeDuration, Color.blue));

                break;
           
            case _statusEffects.burned:

                StartCoroutine(WhileStatIsOn(damageManager.burnDuration, Color.yellow));

                break;
            
            case _statusEffects.stunned:

                StartCoroutine(WhileStatIsOn(damageManager.burnDuration, Color.yellow));

                break;
            
            case _statusEffects.pushedBack:

                StartCoroutine(PushedState());

                break;
            
        }
    }

    private IEnumerator WhileStatIsOn(float duration, Color color)
    {
        controller.StartCoroutine(controller.EnemyStatusEffectFeedback(duration, color));
        yield return new WaitForSeconds(duration);
        actualStatusEffect = _statusEffects.normal;
    }
    private IEnumerator PushedState()
    {
        movement.PushEnemyBack(damageManager.windForce);
        yield return new WaitForSeconds(0.5f);
        actualStatusEffect = _statusEffects.normal;
    }
}
