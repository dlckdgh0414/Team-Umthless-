using UnityEngine;

public class Chicken : Entity
{
    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    protected override void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, 0);
        _renderer.FlipController(dir.x);
    }

    public override void HackingExit()
    {
        _player = null;
        _canMove = false;
    }
}
