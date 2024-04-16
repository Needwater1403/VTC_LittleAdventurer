using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlAnimator : MonoBehaviour
{
    private CharacterManager _characterManager;
    private MaterialPropertyBlock _material;
    private SkinnedMeshRenderer _renderer;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
    private static readonly int isFall = Animator.StringToHash("isFall");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int isDead = Animator.StringToHash("isDead");
    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
        _material = new MaterialPropertyBlock();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _renderer.GetPropertyBlock(_material);
    }

    protected void UpdateAnimation(float veloX, float veloY, bool isAttacking = false)
    {
        _characterManager._animator.SetFloat(VelocityX, veloX);
        _characterManager._animator.SetFloat(VelocityZ, veloY);
        _characterManager._animator.SetBool(isFall,!_characterManager._characterController.isGrounded);
        if (!isAttacking) return;
        ReceiveInput.Instance.isAttacking = false;
        _characterManager._animator.SetTrigger(Attack);
    }
    protected void AIUpdateAnimation(float veloX, float veloY, bool _isDead, bool isAttacking = false)
    {
        if (_isDead)
        {    
            _characterManager._animator.SetTrigger(isDead);
        }
        else if (isAttacking)
        {
            _characterManager._animator.SetTrigger(Attack);
        }
        else
        {
            _characterManager._animator.SetFloat(VelocityX, veloX);
            _characterManager._animator.SetFloat(VelocityZ, veloY);
            _characterManager._animator.SetBool(isFall, _characterManager._characterController.isGrounded);
        }
    }

    #region ShaderVFX
    public IEnumerator MaterialBlink()
    {
        _material.SetFloat("_blink",0.55f);
        _renderer.SetPropertyBlock(_material);
        yield return new WaitForSeconds(0.2f);
        _material.SetFloat("_blink",0);
        _renderer.SetPropertyBlock(_material);
    }
    IEnumerator MaterialDissolve()
    {
        yield return new WaitForSeconds(0.75f);
        float duration = 2f;
        float time = 0;
        float dissolveStart = 20f;
        float dissolveEnd = -10f;
        float dissolve;
        _material.SetFloat("_enableDissolve",1f);
        _renderer.SetPropertyBlock(_material);
        while (time < duration)
        {
            time += Time.deltaTime;
            dissolve = Mathf.Lerp(dissolveStart, dissolveEnd, time / duration);
            _material.SetFloat("_dissolve_height", dissolve);
            _renderer.SetPropertyBlock(_material);
            yield return null;
        }
        Destroy(gameObject);
    }
    protected virtual void DestroyObject()
    {
        StartCoroutine(MaterialDissolve());
    }
    
    #endregion
}
