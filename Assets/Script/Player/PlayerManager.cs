using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public ControlMovement _controlMovement;
    [HideInInspector] public ControlAnimator _controlAnimator;
    [SerializeField] private Health health;
    private bool isDead;
    public bool IsDead => isDead;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(transform.parent);
    }

    protected override void Update()
    {
        if(isDead) return;
        base.Update();
        if (_controlCombat.health.CurrentHp <= 0)
        {
            isDead = true;
            _controlAnimator.isDead = true;
        }
        if (_controlMovement)
        {
            _controlMovement.HandleAllMovement();
        }
        if (_controlAnimator)
        {
            _controlAnimator.HandleAllAnimation();
        }
    }
    // protected override void LateUpdate()
    // {
    //     base.LateUpdate();
    //     PlayerCamera.Instance.HandleCamera();
    // }
    public float GetMaxHP()
    {
        return health.configCombat.maxHP;
    }
    public float GetCurrentHP()
    {
        return health.CurrentHp;
    }
    
}
