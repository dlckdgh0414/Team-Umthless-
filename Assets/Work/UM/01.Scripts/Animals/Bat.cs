using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bat : Entity
{
    [SerializeField] private float _flyPower;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsInvisible;

    private List<VisibleWall> _visibleWalls;
    private Collider2D[] _colliders;

    protected override void Awake()
    {
        base.Awake();
        _visibleWalls = FindObjectsOfType<VisibleWall>().ToList();
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpChargingEvent += Fly;
        OnHackingEnterEvent?.Invoke();
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

        foreach (VisibleWall wall in _visibleWalls)
        {
            if (Array.Exists(_colliders, collider => collider.gameObject == wall.gameObject))
            {

                wall.IsVisible.Value = true;
            }
            else
                wall.IsVisible.Value = false;
        }
    }

    private void Fly(bool isFly)
    {
        RigidCompo.velocity = Vector2.zero;

        if (isFly)
        {
            RigidCompo.AddForce(Vector2.up * _flyPower, ForceMode2D.Impulse);
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player.InputComp.OnJumpChargingEvent -= Fly;
        OnHackingEnterEvent?.Invoke();
        _player = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
