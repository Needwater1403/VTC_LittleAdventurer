using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlCombat : MonoBehaviour
{
    [HideInInspector] public Health health;
    [HideInInspector] public List<Collider> targetList;
    public List<DamageCollider> colliderList;
    
    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        targetList = new List<Collider>();
        SetCollider();
    }

    protected virtual void InflictDamage()
    {
        SetCollider(true);
    }
    protected virtual void ResetTargetList()
    {
        targetList.Clear();
        SetCollider();
    }
    

    private void SetCollider(bool status = false)
    {
        foreach (var dc in colliderList)
        {
            dc.GetComponent<Collider>().enabled = status;
        }
    }
}
