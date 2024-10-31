using UnityEngine;

public class Rhino : Entity
{

    [SerializeField] private AnimTypeSO moveType;

    private float _dashPower = 12f;

    public bool _isDashing;
    private float _currentTime, _dashTime = 1f;

    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpEvent += Jump;
        _player.InputComp.OnSkillEvent += Dash;

        _canMove = true;
    }

    private void Update()
    {
        if (_isDashing)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _dashTime)
            {
                _canMove = true;
                _isDashing = false;
                _currentTime = 0f;
            }
        }

        //if(_player.)
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Move(Vector2 dir)
    {
        base.Move(dir);
        if (dir.x != 0)
        {
            AnimCompo.SetParam(moveType, true);
        }
        else
        {
            AnimCompo.SetParam(moveType, false);
        }
    }

    public override void HackingExit()
    {
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
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
