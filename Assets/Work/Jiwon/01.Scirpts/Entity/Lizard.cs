using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lizard : Entity
{
    private WallCheck _wallCheck;
    protected override void Awake()
    {
        base.Awake();
        _wallCheck = GetComponentInChildren<WallCheck>();
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnSkillEvent += HandleWallRunEvent;
    }

    private void HandleWallRunEvent()
    {
        
    }

    public override void HackingExit()
    {
        _player.InputComp.OnSkillEvent -= HandleWallRunEvent;
    }
}
