using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabit : Entity
{
    [SerializeField] private AnimTypeSO _moveType;
    [SerializeField] private AnimTypeSO _jumpType;
    [SerializeField] private AnimTypeSO _failType;
    [SerializeField] private float _jumpChraging;
    private float jumpPower;
    private bool IsCharging;
    private bool _isCharging;

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
        if (RigidCompo.velocity.y < 0)
        {
            AnimCompo.SetParam(_jumpType, false);
            AnimCompo.SetParam(_failType, true);
        }

        if (CheckCompo.IsGround)
        {
            AnimCompo.SetParam(_failType, false);
        }

        if (IsCharging)
        {
            _canMove = false;
            Move(Vector2.zero);
            ChargingJump();
        }

        if (RigidCompo.velocity.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
        }

        if (_isCharging)
        {
            jumpPower += Time.deltaTime * _jumpChraging;
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

    public override void HackingExit()
    {
        _player.InputComp.OnJumpChargingEvent -= Jump;
        _player = null;
        
        _canMove = false;
    }

    private void Jump(bool isCharging)
    {
        IsCharging = isCharging;
        if (!isCharging && CheckCompo.IsGround)
        {
            _isCharging = false;
            Debug.Log("점프");
            AnimCompo.SetParam(_jumpType, true);
            RigidCompo.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpPower = _moveData.jumpPower;
        }
       
        if (!IsCharging)
        {
            _canMove = true;
            jumpPower = _moveData.jumpPower;
        }
    }

    private void ChargingJump()
    {
          _isCharging = true;
          if (jumpPower > 10)
          {
              jumpPower = 10;
              _isCharging = false;
          }
    }
}
