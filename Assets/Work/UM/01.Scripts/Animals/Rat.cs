public class Rat : Entity
{
    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpEvent += Jump;
        _canMove = true;
    }

    public override void HackingExit()
    {
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
        _canMove = false;
    }
}
