using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterHealth : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;

    public class CharacterDeathEventArgs: EventArgs
    {
        public bool isBrutal;
    }
    public event EventHandler<CharacterDeathEventArgs> OnCharacterDeath;
    public virtual void Initialize()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage, bool brutal = false)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Die(brutal);
        }
    }
    protected virtual void Die(bool brutal = false)
    {
        OnCharacterDeath?.Invoke(this, new CharacterDeathEventArgs { isBrutal=brutal});
        Destroy(gameObject);
    }
}
