using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabit : Entity
{
    [SerializeField] private AnimTypeSO _moveType;
    private float jumpPower;
    private bool IsCharging;

    protected override void Awake()
    {
        base.Awake();
        
    }

    private void Start()
    {
        jumpPower = _moveData.jumpPower;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpChargingEvent += Jump;
        _canMove = true;
    }

    private void Update()
    {
        if (IsCharging)
        {
            _canMove = false;
            Move(Vector2.zero);
            ChargingJump();
        }
        else
        {
            _canMove = true;
            jumpPower = _moveData.jumpPower;
        }
    }

    public override void HackingExit()
    {
        _player = null;
        _player.InputComp.OnJumpChargingEvent -= Jump;
        _canMove = false;
    }

    private void Jump(bool isCharging)
    {
        IsCharging = isCharging;
        if (!isCharging)
        {
            RigidCompo.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpPower = _moveData.jumpPower;
        }
    }

    protected override void Move(Vector2 dir)
    {
        base.Move(dir);
        if(dir.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
        }
    }

    private void ChargingJump()
    {
          StartCoroutine(ChargingCoroutine());
          if (jumpPower > 10)
          {
              jumpPower = 10;
          }
    }

    private IEnumerator ChargingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        jumpPower += 0.5f;
    }
}
