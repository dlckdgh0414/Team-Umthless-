using UnityEngine;

public class Rat : Entity
{
    [SerializeField] private AnimTypeSO _moveType;
    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    protected override void Move(Vector2 dir)
    {
        base.Move(dir);
        if (dir.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
        }
    }

    public override void HackingExit()
    {
        _player = null;
        _canMove = false;
    }
}
