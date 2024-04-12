using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [SerializeField] protected ControlMovement _controlMovement;
    [SerializeField] protected ControlAnimator _controlAnimator;
    [SerializeField] protected ControlCombat _controlCombat;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    protected override void Update()
    {
        base.Update();
        if (_controlMovement)
        {
            _controlMovement.HandleAllMovement();
        }
        if (_controlAnimator)
        {
            _controlAnimator.HandleAllAnimation();
        }
        if (_controlCombat)
        {
            _controlCombat.HandleAllCombat();
        }
    }
    // protected override void LateUpdate()
    // {
    //     base.LateUpdate();
    //     PlayerCamera.Instance.HandleCamera();
    // }
}
