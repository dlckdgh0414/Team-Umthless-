using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lizard : Entity
{
    [Header("LizardSetting")] 
    [SerializeField] private float lizardToWallJump;
    
    private WallCheck _wallCheck;
    private bool _isWallRen;
    
    protected override void Awake()
    {
        base.Awake();
        _wallCheck = GetComponentInChildren<WallCheck>();
        _wallCheck.Initialized(this);
        _isWallRen = false;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnSkillEvent += HandleWallRunEvent;
        _player.InputComp.OnJumpEvent += Jump;
    }

    private void HandleWallRunEvent()
    {
        if (_isWallRen)
        {
            if (_wallCheck.IsWallCheck())
            {
                int wallDir = _wallCheck.isRightWall ? 90 : -90;
                
                RigidCompo.AddForce(new Vector2(Mathf.Sign(wallDir),1) * lizardToWallJump, ForceMode2D.Impulse);
                transform.eulerAngles = new Vector3(0, 0, wallDir);
            }
        }
    }

    public override void HackingExit()
    {
        _player.InputComp.OnSkillEvent -= HandleWallRunEvent;
        _player.InputComp.OnJumpEvent -= Jump;
    }
}
