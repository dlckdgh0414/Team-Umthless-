using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lizard : Entity
{
    [Header("LizardSetting")] 
    [SerializeField] private float lizardToWallJump;

    [SerializeField] private AnimTypeSO _moveType;
    
    private WallCheck _wallCheck;
    private bool _isWallRen;
    private bool _isWallRight;

    private float _wallMoveDir;
    
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
        _canMove = true;
    }
    
    private void HandleWallRunEvent()
    {
        if (!_isWallRen)
        {
            if (_wallCheck.IsWallCheck())
            {
                int wallDir = Mathf.Sign(_renderer.FacingDirection) > 0  ? 90 : -90;
                _isWallRight = Mathf.Sign(_renderer.FacingDirection) > 0;
                
                RigidCompo.AddForce(new Vector2(Mathf.Sign(wallDir),1) * lizardToWallJump, ForceMode2D.Impulse);
                transform.eulerAngles = new Vector3(0, 0, wallDir);
                _canMove = false;
                _isWallRen = true;
                RigidCompo.gravityScale = 0;
            }
        }
        else if (_isWallRen)
        {
            OffWallRun();
        }
    }

    protected override void Move(Vector2 dir)
    {
        base.Move(dir);
        if (dir.x != 0)
        {
            AnimCompo.SetParam(_moveType,true);
        }
        else
        {
            AnimCompo.SetParam(_moveType,false);
        }
    }

    private void OffWallRun()
    {
        RigidCompo.gravityScale = 1;
        RigidCompo.AddForce(Vector2.up * lizardToWallJump, ForceMode2D.Impulse);
        transform.eulerAngles = new Vector3(0, 0, 0);
        _canMove = true;
        _isWallRen = false;
    }

    private void WallMove(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(RigidCompo.velocity.x, dir.y * _moveData.moveSpeed);
        if (dir.y != 0)
        {
            AnimCompo.SetParam(_moveType,true);
        }
        else if (dir.y == 0)
        {
            AnimCompo.SetParam(_moveType,false);
        }
    }

    private void Update()
    {
        if (_isWallRen)
        {
            if (!_wallCheck.IsWallRuningCheck())
            {
                OffWallRun();
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_isWallRen)
        {
            WallMove(_player.InputComp.MoveDir);
            if (!_isWallRight)
            {
                _renderer.FlipController(-_player.InputComp.MoveDir.y);
                
            }
            else if (_isWallRight)
                _renderer.FlipController(_player.InputComp.MoveDir.y);
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player.InputComp.OnSkillEvent -= HandleWallRunEvent;
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
    }
}
