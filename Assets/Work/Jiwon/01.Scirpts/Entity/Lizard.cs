using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lizard : Entity
{
    private WallCheck _wallCheck;
    private bool _isWallRen;
    
    protected override void Awake()
    {
        base.Awake();
        _wallCheck = GetComponentInChildren<WallCheck>();
        _isWallRen = false;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnSkillEvent += HandleWallRunEvent;
    }

    private void HandleWallRunEvent()
    {
        if (_isWallRen)
        {
            if (_wallCheck.IsWallCheck())
            {
                
            }
        }
    }

    public override void HackingExit()
    {
        _player.InputComp.OnSkillEvent -= HandleWallRunEvent;
    }
}
