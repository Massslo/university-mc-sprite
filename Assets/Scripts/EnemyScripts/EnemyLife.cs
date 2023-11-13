using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : Stats
{   
    private void Awake()
    {
        _health = 30.0f;
        _armor = 1.0f;
        _strength = 10.0f;
        _attackSpeed = 2.0f;
    }

    public override void Die()
    {
        Debug.Log("Enemy is dead!");
        Destroy(this);
    }
}
