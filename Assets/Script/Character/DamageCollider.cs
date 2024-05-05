using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private CharacterControlCombat _controlCombat;
    [SerializeField] private Transform parent;
    private Collider _collider;
    private float _damageBonus = 0;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_controlCombat.health.configCombat.targetTag) && !_controlCombat.targetList.Contains(other))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                if (parent.CompareTag(Constants.PlayerTag)) _damageBonus = GameManager.Instance.Player.DamageBonus;
                health.TakeDamage(_controlCombat.health.configCombat.normalATK + _damageBonus);
                Debug.Log($"<color=red>{health.gameObject.tag}</color> Current HP: {health.CurrentHp}");
                InitSlashVFX();
                InitBeingHitVFX(other);
                Blink(other);
                InitHitAudio(other);
            }        
            _controlCombat.targetList.Add(other);
        }
    }

    private void InitHitAudio(Component other)
    {
        if (other.CompareTag(Constants.EnemyTag))
        {
            other.GetComponent<AudioManager>().PlayAudio(Constants.BladeHit);
        }
    }

    private void InitSlashVFX()
    {
        if (parent.CompareTag(Constants.PlayerTag))
        {
            var _controlAnimator = parent.GetComponent<ControlAnimator>();
            if (_controlAnimator == null) return;
            RaycastHit hit;
            var pos = transform.position + (-_collider.bounds.extents.y) * parent.transform.up;
            var isHit = Physics.BoxCast(pos, _collider.bounds.extents/2, parent.transform.up, out hit,
                transform.rotation, _collider.bounds.extents.y*2 , 1 << 6);
            if (isHit)
            {
                _controlAnimator.SlashVFX(hit.point + new Vector3(0,1f,0));
            }
        }
    }

    private void InitBeingHitVFX(Component other)
    {
        if (other.CompareTag(Constants.EnemyTag))
        {
            var _controlAnimator = other.GetComponent<AICharacterControlAnimator>();
            if (_controlAnimator != null)
            {
                _controlAnimator.BeingHitVFX(parent.transform.position);
            }
        }
    }
    private void Blink(Component other)
    {
        var _controlAnimator = other.GetComponent<CharacterControlAnimator>();
        StartCoroutine(_controlAnimator.MaterialBlink());
    }
}
