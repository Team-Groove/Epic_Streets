using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokerController : EnemyController
{
    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();

        if (sceneController.endGameNum == 1)
        {
            maxhealth *= 1.5f;
        }

        player = FindObjectOfType<PlayerController>().transform;
        playersPos = player.transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        healthBar.SetMaxHealth(maxhealth);

        if (!isDummy)
        {
            ia = GetComponent<EnemyIA>();
        }
    }
    public void PlayDeathScream()
    {
        AudioManager.instance.Play("BossDeathScream");
    }
    public void PlaySmokeCigarrete()
    {
        AudioManager.instance.Play("BossExhales");
    }
    public void PlayAttackSound()
    {
        AudioManager.instance.Play("BossScream");
    }
    public void PlayChargeAttack()
    {
        AudioManager.instance.Play("ChargeAttack", .6f);
    }
    public void StopChargeAttack()
    {
        AudioManager.instance.Stop("ChargeAttack", .6f);
    }
    public void PlayStepSound_1()
    {
        AudioManager.instance.Play("StepSounds_1", .3f);
    }
    public void PlayStepSound_2()
    {
        AudioManager.instance.Play("StepSounds_2", .3f);
    }
    public void PlayStepSound_3()
    {
        AudioManager.instance.Play("StepSounds_3", .3f);
    }

}
