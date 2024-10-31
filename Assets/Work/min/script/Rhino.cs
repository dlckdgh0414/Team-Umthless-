using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rhino : Entity
{
    private float _dashPower = 12f;

    public bool _isDashing;
    private float _currentTime, _dashTime = 1f;

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpEvent += Jump;
        _player.InputComp.OnSkillEvent += Dash;

        _canMove = true;
        Debug.Log("ÀÔ°¶");
    }

    private void Update()
    {
        if (_isDashing)
        {
            _currentTime += Time.deltaTime;

            if(_currentTime > _dashTime)
            {
                _canMove = true;
                _isDashing = false;
                _currentTime = 0f;
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void HackingExit()
    {
        _player = null;
        _player.InputComp.OnJumpEvent -= Jump;
    }

    private void Dash()
    {
        _canMove = false;
        _isDashing = true;
        RigidCompo.AddForce(new Vector2(_renderer.FacingDirection * _dashPower,
            RigidCompo.velocity.y), ForceMode2D.Impulse);
    }

    public void ResetVelocity()
    {
        RigidCompo.velocity = Vector2.zero;
    }
}
