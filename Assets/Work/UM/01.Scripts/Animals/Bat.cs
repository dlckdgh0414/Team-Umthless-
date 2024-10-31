using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Entity
{
    [SerializeField] private float _flyPower;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsInvisible;

    private Collider2D[] _colliders;

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

    private void Update()
    {
        if (!_canMove) return;

        CheckInvisibleWall();
    }

    private void CheckInvisibleWall()
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _whatIsInvisible);
        
        foreach (Collider2D collider in _colliders)
        {
            if (collider.gameObject.TryGetComponent(out VisibleWall visible))
            {
                visible.IsVisible.Value = true;
            }
        }
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
            RigidCompo.AddForce(Vector2.down * _flyPower * 1.2f, ForceMode2D.Impulse);
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
    }
}
