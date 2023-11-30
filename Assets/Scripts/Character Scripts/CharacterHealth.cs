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
        public CharacterManager manager;
        public bool isBrutal;
    }
    public event EventHandler<CharacterDeathEventArgs> OnCharacterDeath;
    public event EventHandler OnCharacterHit;
    public virtual void Initialize()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage, bool brutal = false)
    {
        OnCharacterHit?.Invoke(this, new EventArgs());
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Die(brutal);
        }
    }
    protected virtual void Die(bool brutal = false)
    {
        OnCharacterDeath?.Invoke(this, new CharacterDeathEventArgs { manager = GetComponent<CharacterManager>(), isBrutal=brutal});
        Destroy(gameObject);
    }
    public virtual void Heal() { }
}
