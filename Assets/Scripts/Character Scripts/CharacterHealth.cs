using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterHealth : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

    public event EventHandler OnCharacterDeath;
    public void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        OnCharacterDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
