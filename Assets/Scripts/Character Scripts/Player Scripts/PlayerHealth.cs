using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : CharacterHealth
{
    [SerializeField] Slider healthBar;
    [SerializeField] Slider backgroundHealthbar;
    [SerializeField] float healthBarAnimationDelay;
    [SerializeField] float healthBarSpeed;
    bool healthBarCoroutineIsRunning;
    public override void Initialize()
    {
        base.Initialize();
        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;
        backgroundHealthbar.maxValue = maxHP;
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
}
