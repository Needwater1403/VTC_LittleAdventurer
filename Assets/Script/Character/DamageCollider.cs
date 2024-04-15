using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private CharacterControlCombat _controlCombat;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_controlCombat.health.configCombat.targetTag) && !_controlCombat.targetList.Contains(other))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(_controlCombat.health.configCombat.normalATK);
                Debug.Log($"{health.gameObject.tag} Current HP: {health.CurrentHp}" );
            }
            _controlCombat.targetList.Add(other);
        }
    }
}
