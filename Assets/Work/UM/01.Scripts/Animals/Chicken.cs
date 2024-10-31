using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Entity
{
    [SerializeField] private float gravityValue = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        RigidCompo.gravityScale = gravityValue;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    public override void HackingExit()
    {
        _player = null;
        _canMove = false;
    }
}
