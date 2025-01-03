using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour, IHackingEnter, IHackingExit
{
    protected Dictionary<Type, IEntityComponent> _components;

    public Rigidbody2D RigidCompo { get; protected set; }
    public UnityEvent OnHackingEnterEvent;
    public UnityEvent OnHackingExitEvent;
    protected AnimalsAnim AnimCompo;
    protected Player _player;

    protected EntityRenderer _renderer;
    public GroundCheck CheckCompo { get; protected set; }

    [SerializeField]
    protected bool _canMove;

    protected virtual void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        AnimCompo = GetComponentInChildren<AnimalsAnim>();

        _canMove = false;

        _components = new Dictionary<Type, IEntityComponent>();
        GetComponentsInChildren<IEntityComponent>(true).ToList()
            .ForEach(component => _components.Add(component.GetType(), component));

        InitComponents();

        _renderer = GetComponentInChildren<EntityRenderer>();
        CheckCompo = GetComponentInChildren<GroundCheck>();
    }

    private void InitComponents()
    {
        _components.Values.ToList().ForEach(component => component.Initialize(this));
    }

    public AnimalDataSO _moveData;

    protected virtual void FixedUpdate()
    {
        if (!_canMove) return;
        Move(_player.InputComp.MoveDir);
    }

    protected virtual void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, RigidCompo.velocity.y);
        _renderer?.FlipController(dir.x);
    }

    protected virtual void Jump()
    {
        if (CheckCompo.IsGround)
            RigidCompo.AddForce(Vector2.up * _moveData.jumpPower, ForceMode2D.Impulse);
    }

    public abstract void HackingEnter(Player player);

    public abstract void HackingExit();
}
