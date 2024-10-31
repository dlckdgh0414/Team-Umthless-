using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Entity
{
    private Player _player;
    private float _dashPower = 15f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpEvent += Jump;
    }

    public override void HackingExit()
    {
        _player = null;
        _player.InputComp.OnJumpEvent -= Jump;
    }

    private void Dash()
    {
        _canMove = false;
        RigidCompo.velocity = new Vector2(_dashPower, RigidCompo.velocity.y) ;
    }
}
