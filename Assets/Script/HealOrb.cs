using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealOrb : DropItem
{
    private MaterialPropertyBlock _material;
    private MeshRenderer _renderer;
    protected override void Awake()
    {
        base.Awake();
        _material = new MaterialPropertyBlock();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _renderer.GetPropertyBlock(_material);
    }

    protected override void DropAction(Collider other)
    {
        base.DropAction(other);
        other.GetComponent<Health>().CurrentHp += 10;
        other.GetComponent<PlayerManager>()._controlAnimator.HealVFX();
        Destroy(gameObject);
    }
}
