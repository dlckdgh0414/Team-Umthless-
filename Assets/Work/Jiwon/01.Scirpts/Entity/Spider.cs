using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Spider : Entity
{
    private SpringJoint2D _joint;
    private LineRenderer _line;

    private bool _isSwing;
    private bool _isFire;

    [Header("SpiderSetting")] [SerializeField]
    private Transform firePos;

    [SerializeField] private Transform buillet;
    [SerializeField] private float builletSpeed;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float builletRadius;
    [SerializeField] private float maxBuilletTimer;
    [SerializeField] private float tension;
    private Vector2 _builletDir;
    private float _builletTimer;
    private Vector2 _hitPoint;

    protected override void Awake()
    {
        base.Awake();
        _joint = GetComponent<SpringJoint2D>();
        _line = GetComponent<LineRenderer>();

        _joint.enabled = false;
        buillet.gameObject.SetActive(false);
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
        _player.InputComp.OnSkillEvent += HandleFierEvent;
    }

    private void HandleFierEvent()
    {
        if (!_isFire && !_isSwing)
        {
            buillet.gameObject.SetActive(true);
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mouse = Camera.main.ScreenToWorldPoint(mousePos);
            RigidCompo.velocity = Vector2.zero;
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _builletDir = (mouse - (Vector2)transform.position).normalized;
            _isFire = true;
            _canMove = false;
            _builletTimer = 0;
        }

        else if (_isSwing)
        {
            ResetBuillet();
        }
    }

    private void Update()
    {
        if (_isFire)
        {
            _builletTimer += Time.deltaTime;
            if (_builletTimer >= maxBuilletTimer)
            {
                ResetBuillet();
            }

            Collider2D hitWall = Physics2D.OverlapCircle(buillet.position, builletRadius, whatIsWall);
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, buillet.position);
            if (hitWall != null)
            {
                StuckWap(buillet.position);
            }

            buillet.position += (Vector3)_builletDir * builletSpeed * Time.deltaTime;
        }

        if (_isSwing && _line.positionCount <= 2)
        {
            buillet.position = _hitPoint;
            _line.SetPosition(0, transform.position);
        }
    }

    private void StuckWap(Vector3 hit)
    {
        _canMove = false;
        _isFire = false;
        _isSwing = true;
        _joint.enabled = true;
        _hitPoint = hit;
        _joint.connectedAnchor = _hitPoint;

        float dist = Vector3.Distance(transform.position, hit);
        _joint.distance = dist * tension;
    }

    private void ResetBuillet()
    {
        _isFire = false;
        _isSwing = false;
        _line.positionCount = 0;
        _builletTimer = 0;
        _builletDir = Vector2.zero;
        buillet.position = firePos.position;
        _joint.anchor = Vector2.zero;
        _joint.enabled = false;
        buillet.gameObject.SetActive(false);
        _canMove = true;
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player.InputComp.OnSkillEvent -= HandleFierEvent;
    }

    private void OnDrawGizmosSelected()
    {
        if (buillet != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(buillet.position, builletRadius);
            Gizmos.color = Color.white;
        }
    }
}