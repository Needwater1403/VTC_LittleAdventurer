using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{
    public ConfigCombatSO configCombat;
    private float currentHP;
    public float CurrentHp => currentHP;

    private void Start()
    {
        currentHP = configCombat.maxHP;
    }

    public void TakeDamage(float _damage)
    {
        currentHP -= _damage;
        // CHECK DIE CONDITION
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }
}
