using UnityEngine;

public class Chicken : Entity
{
    [SerializeField] private AnimTypeSO _moveType;
    [SerializeField] private float _a;

    public override void HackingEnter(Player player)
    {
        _player = player;
        OnHackingEnterEvent?.Invoke();
        _canMove = true;
    }

    private void Update()
    {
        if (RigidCompo.velocity.y < 0)
        {

        }

        if (RigidCompo.velocity.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
        }
    }

    protected override void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, _a);
        _renderer.FlipController(dir.x);
    }

    public override void HackingExit()
    {
        _player = null;
        OnHackingEnterEvent?.Invoke();
        _canMove = false;
    }
}
