using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class APE : Entity
{

    public UnityEvent OnHoldingEvent;
    public UnityEvent OnThrowingHoldingEvent;

    [SerializeField] private AnimTypeSO _moveType;
    [SerializeField] private AnimTypeSO _holdType;
    [SerializeField] private AnimTypeSO _holdMoveType;

    [ field : SerializeField] public Transform _holdTrm;
    [SerializeField] private Vector2 _checkSize;
    [SerializeField] private LayerMask _whatIsHoldObj;

    public bool isHold = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnSkillEvent += HoldObj;
        _canMove = true;
    }

    private bool HoldCheck()
    {
        bool holdObj = Physics2D.OverlapBox(transform.position, _checkSize, 0,_whatIsHoldObj);
        return holdObj;
    }

    protected override void Move(Vector2 dir)
    {
        
        base.Move(dir);
        if(dir.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
            if (isHold)
                AnimCompo.SetParam(_holdMoveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
            if (isHold)
                AnimCompo.SetParam(_holdMoveType, false);
        }
       
    }

    private void Update()
    {
        HoldCheck();
    }

    private void HoldObj()
    {
       
        if(HoldCheck())
        {
            AnimCompo.SetParam(_holdType, true);
            OnHoldingEvent?.Invoke();
           
        }
        else
        {
            OnThrowingHoldingEvent?.Invoke();
            isHold = false;
            AnimCompo.SetParam(_holdType, false);
        }
    }

    public override void HackingExit()
    {
        _player = null;
        _player.InputComp.OnSkillEvent -= HoldObj;
        _canMove = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _checkSize);
    }
}
