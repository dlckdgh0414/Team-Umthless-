using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Entity
{
    protected override void Awake()
    {
        base.Awake();

        RigidCompo.gravityScale = 0;
    }

    public override void HackingEnter(Player player)
    {
        _canMove = true;
    }

    protected override void Move(Vector2 dir)
    {
        RigidCompo.velocity = dir;
    }

    public override void HackingExit()
    {
        _canMove = false;
    }
}
