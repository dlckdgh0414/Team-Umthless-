public class Rat : Entity
{
    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    public override void HackingExit()
    {
        _player = null;
        _canMove = false;
    }
}
