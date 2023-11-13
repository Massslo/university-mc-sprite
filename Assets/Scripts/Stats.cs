using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    protected float _health;
    protected float _armor;
    protected float _strength;
    protected float _attackSpeed;

    protected bool _isAttacking;
    protected bool _isInCombat;

    private float _nextDamageEvent;

    public virtual void Die()
    {

    }

    public float AttackSpeed
    {
        get{ return _attackSpeed;}
        set
        {
            _attackSpeed = value;
        }
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage * _armor;
        //Debug.Log(_health);
        if(_health <= 0)
        {
            Die();
        }
    }

    public float DoDamage()
    {
        Debug.Log("HITHITHIT");
        return _strength;
    }

    
}
