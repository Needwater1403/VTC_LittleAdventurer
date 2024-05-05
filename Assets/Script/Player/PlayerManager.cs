using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    public ControlMovement _controlMovement;
    public ControlAnimator _controlAnimator;
    [SerializeField] private Health health;
    public Health Health => health;
    private float coinNum = 0;
    public float CoinNum => coinNum;
    private bool isDead;
    public bool IsDead => isDead;

    private float damageBonus;
    public float DamageBonus
    {
        get => damageBonus;
        set => damageBonus = value;
    }

    private float lastGetAttackBonusTime;
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(transform.parent);
    }

    protected override void Update()
    {
        HandleAttackBuff();
        if(isDead) return;
        base.Update();
        
        if (_controlCombat.health.CurrentHp <= 0)
        {
            isDead = true;
            _controlAnimator.isDead = true;
        }
        if (_controlAnimator)
        {
            _controlAnimator.HandleAllAnimation(isPaused);
        }

        if (isPaused) return;
        if (_controlMovement)
        {
            _controlMovement.HandleAllMovement();
        }
    }
    public float GetMaxHP()
    {
        return health.configCombat.maxHP;
    }
    public float GetCurrentHP()
    {
        return health.CurrentHp;
    }
    public void AddCoin(int amount)
    {
        coinNum += amount;
    }

    public void Pause(bool pause)
    {
        isPaused = pause;
    }

    private void HandleAttackBuff()
    {
        if (damageBonus == 0)
        {
            lastGetAttackBonusTime = Time.realtimeSinceStartup;
            return;
        }
        if (Time.realtimeSinceStartup - lastGetAttackBonusTime < 5) return;
        damageBonus = 0;
        lastGetAttackBonusTime = Time.realtimeSinceStartup;
        Debug.Log("ATKBuff end!");
    }
}
