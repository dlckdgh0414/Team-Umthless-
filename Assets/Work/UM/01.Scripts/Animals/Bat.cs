using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Entity
{
    [SerializeField] private float _flyPower;
    protected override void Awake()
    {
        base.Awake();

        RigidCompo.gravityScale = 0;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpChargingEvent += Fly;
        _canMove = true;
    }

    private void Fly(bool isFly)
    {
        RigidCompo.velocity = Vector2.zero;

        if (isFly)
        {
            RigidCompo.AddForce(Vector2.up * _flyPower, ForceMode2D.Impulse);
        }
        else
        {
            RigidCompo.AddForce(Vector2.down * _flyPower * 2, ForceMode2D.Impulse);
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
    }
}
