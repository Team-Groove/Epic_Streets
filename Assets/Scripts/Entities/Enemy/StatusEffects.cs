using System.Collections;
using UnityEngine;
using TMPro;


public enum _statusEffects {normal, poisoned, freezed, burned, pushedBack }

public class StatusEffects : MonoBehaviour
{
    private EnemyMovement movement;
 
    private AttackDamageManager damageManager;
    private EnemyController controller;

    public bool isStatusOn;

    [SerializeField] private GameObject popUpNumber;
    [SerializeField] private GameObject popUpNumberSpawnPoint;

    [SerializeField] private GameObject popUpStatusName;
   
    public _statusEffects actualStatusEffect;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        //hurtbox = GetComponent<EnemyHurtBox>();
        //ia = GetComponent<EnemyIA>();
        controller = GetComponent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
    }

    private void Start()
    {
        popUpStatusName.SetActive(false);
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

                isStatusOn = false;

                break;
            
            case _statusEffects.poisoned:

                Poisoned();

                break;
            
            case _statusEffects.freezed:

                Frozen();

                break;
           
            case _statusEffects.burned:

                Burned();

                break;
            
 
            
            case _statusEffects.pushedBack:

                PushedBack();

                break;
            
        }
    }

    private IEnumerator PushedState(float duration, Color color)
    {
        SetStatusNamePopUp("Pushed", color);
     
        controller.EnemyStatusEffectFeedback(color);

        yield return new WaitForSeconds(duration);

        popUpStatusName.SetActive(false);

        controller.EnemyNoStatusFeedback();

        actualStatusEffect = _statusEffects.normal;
    }
    private IEnumerator PoisonedStatus(Color color, float poisonDamage, float damagePerLoop, float interval)
    {
        float amountDamaged = 0;
        
        controller.EnemyStatusEffectFeedback(color);
        popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + damagePerLoop.ToString());
        popUpNumber.GetComponentInChildren<TextMeshPro>().color = color;

        while (amountDamaged <= poisonDamage)
        {
            SetStatusNamePopUp("Poisoned", color);
            
            controller.ReceiveDamage(damagePerLoop);
         
            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);
            

            amountDamaged += damagePerLoop;
            
            yield return new WaitForSeconds(interval);
        }

        popUpStatusName.SetActive(false);
        controller.EnemyNoStatusFeedback();
        actualStatusEffect = _statusEffects.normal;
    }
    private IEnumerator FrozenStatus(float duration, Color color, float slow)
    {
        SetStatusNamePopUp("Frozen", color);
        Vector2 a = new Vector2(controller.horizontal_speed, controller.vertical_speed);
      
        controller.EnemyStatusEffectFeedback(color);
  
        controller.horizontal_speed /= slow;
        controller.vertical_speed /= slow;

        yield return new WaitForSeconds(duration);

        popUpStatusName.SetActive(false);
        
        controller.horizontal_speed = a.x;
        controller.vertical_speed = a.y;

        controller.EnemyNoStatusFeedback();
        actualStatusEffect = _statusEffects.normal;
    }
    private IEnumerator BurnedStatus(Color color, float Damage, float damagePerLoop, float interval)
    {
        float amountDamaged = 0;
       
        controller.EnemyStatusEffectFeedback(color);
        popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + damagePerLoop.ToString());
        popUpNumber.GetComponentInChildren<TextMeshPro>().color = color;

        while (amountDamaged <= Damage)
        {
            SetStatusNamePopUp("Burning", color);

            controller.ReceiveDamage(damagePerLoop);

            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(interval);

            amountDamaged += damagePerLoop;
        }

        popUpStatusName.SetActive(false);

        controller.EnemyNoStatusFeedback();
        actualStatusEffect = _statusEffects.normal;
    }
    public void Poisoned()
    {
        if (!isStatusOn)
        {
            isStatusOn = true;
            StartCoroutine(PoisonedStatus(
                    Color.green, damageManager.poisonTotalDamage, damageManager.poisonDamagePerLoop, damageManager.poisonInterval));
        }
        
    }
    public void Frozen()
    {
        if (!isStatusOn)
        {
            isStatusOn = true;
            StartCoroutine(FrozenStatus(damageManager.freezeDuration,
                    Color.cyan, damageManager.slowAmount));
        }
    }
    public void Burned()
    {
        if (!isStatusOn)
        {
            isStatusOn = true;
            StartCoroutine(BurnedStatus( new Color(1f, 120f / 255f, 49 / 255f), damageManager.burnTotalDamage, damageManager.burnDamagePerLoop, damageManager.burnInterval));
        }
    }
    public void PushedBack()
    {
        if (!isStatusOn)
        {
            isStatusOn = true;
            StartCoroutine(PushedState(0.5f, Color.grey));
        }
    }
    private void SetStatusNamePopUp(string statusName, Color color)
    {
        popUpStatusName.GetComponentInChildren<TextMeshPro>().text = statusName;
        popUpStatusName.GetComponentInChildren<TextMeshPro>().color = color;

        popUpStatusName.SetActive(true);
    }



}
