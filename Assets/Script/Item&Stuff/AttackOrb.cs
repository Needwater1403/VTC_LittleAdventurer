using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrb : DropItem
{
    private MaterialPropertyBlock _material;
    private MeshRenderer _renderer;
    public float damageBonus;
    protected override void Awake()
    {
        base.Awake();
        _material = new MaterialPropertyBlock();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _renderer.GetPropertyBlock(_material);
    }

    protected override void DropAction()
    {
        GameManager.Instance.Player.DamageBonus = damageBonus;
        GameManager.Instance.Player._controlAnimator.HealVFX();
        base.DropAction();
    }
}
