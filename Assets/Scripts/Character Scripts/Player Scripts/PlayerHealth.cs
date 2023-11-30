using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using FMODUnity;
public class PlayerHealth : CharacterHealth
{
    [SerializeField] Slider healthBar;
    [SerializeField] Slider backgroundHealthbar;
    [SerializeField] float healthBarAnimationDelay;
    [SerializeField] float healthBarSpeed;
    bool healthBarCoroutineIsRunning;

    public System.EventHandler OnPlayerDeath;
    public EventReference PlayerDeathSound; 
    public override void Initialize()
    {
        base.Initialize();
        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;
        backgroundHealthbar.maxValue = maxHP;
        backgroundHealthbar.value = maxHP;
        GameHandler.instance.OnPlayerRestart += Instance_OnPlayerRestart;
    }

    private void Instance_OnPlayerRestart(object sender, EventArgs e)
    {
        currentHP = maxHP;
        healthBar.value = maxHP;
        backgroundHealthbar.value = maxHP;
    }

    public override void TakeDamage(int damage, bool brutal = false)
    {
        base.TakeDamage(damage, brutal);
        healthBar.value = currentHP;
        if (!healthBarCoroutineIsRunning)
        {
            StartCoroutine("HealthBarChipCoroutine");
        }
    }
    public override void Heal()
    {
        base.Heal();
        currentHP = maxHP;
        StartCoroutine("HealCoroutine");
    }
    IEnumerator HealthBarChipCoroutine()
    {
        healthBarCoroutineIsRunning = true;
        yield return new WaitForSeconds(healthBarAnimationDelay);
        while(backgroundHealthbar.value > currentHP)
        {
            backgroundHealthbar.value -= healthBarSpeed*Time.deltaTime;
            yield return null;
        }
        backgroundHealthbar.value = currentHP;

        healthBarCoroutineIsRunning = false;
    }
    IEnumerator HealCoroutine()
    {
        healthBarCoroutineIsRunning = true;
        while (healthBar.value < currentHP)
        {
            healthBar.value += healthBarSpeed * Time.deltaTime;
            yield return null;
        }
        healthBar.value = currentHP;
        backgroundHealthbar.value = currentHP;

        healthBarCoroutineIsRunning = false;
    }

    public static void SyncPlayerHealth(PlayerHealth copyFrom, PlayerHealth copyTo)
    {
        copyTo.backgroundHealthbar.value = copyTo.healthBar.value = copyTo.currentHP = copyFrom.currentHP;
    }
    protected override void Die(bool brutal = false)
    {
        AudioManager.instance.Play(PlayerDeathSound);
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
}
