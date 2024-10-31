using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHackingEnter, IHackingExit
{
    protected Dictionary<Type, IEntityComponent> _components;

    protected Rigidbody2D RigidCompo;
    protected Player _player;

    [HideInInspector]
    public bool _canMove;

    protected virtual void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();

        _canMove = false;

        _components = new Dictionary<Type, IEntityComponent>();
        GetComponentsInChildren<IEntityComponent>(true).ToList()
            .ForEach(component => _components.Add(component.GetType(), component));

        InitComponents();
    }

    private void InitComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    [SerializeField] protected MovementDataSO _moveData;

    private void FixedUpdate()
    {
        if (!_canMove) return;
        Debug.Log(_canMove);
        Move(_player.InputComp.MoveDir);
    }

    protected virtual void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, RigidCompo.velocity.y);
        Debug.Log(dir);
    }

    protected virtual void Jump()
    {
        RigidCompo.AddForce(Vector2.up * _moveData.jumpPower, ForceMode2D.Impulse);
    }

    public abstract void HackingEnter(Player player);

    public abstract void HackingExit();
}
