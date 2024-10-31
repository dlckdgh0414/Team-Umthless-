using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabit : Entity
{

    private float _changingPrees;

    protected override void Awake()
    {
        base.Awake();
        
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpChargingEvent += OnJumpChanging;
        _canMove = true;
    }

    public override void HackingExit()
    {
        _player = null;
        _player.InputComp.OnJumpChargingEvent -= OnJumpChanging;
        _canMove = false;
    }

    private void OnJumpChanging(bool isTrue)
    {
        if (isTrue)
        {
           while(_changingPrees > 7)
            {
                _changingPrees += 0.5f;
            }
        }
        else
        {
            _changingPrees = 0;
        }
    }

    protected override void Jump()
    {
        base.Jump();
        float jumpPower = _moveData.jumpPower + _changingPrees;
        RigidCompo.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
