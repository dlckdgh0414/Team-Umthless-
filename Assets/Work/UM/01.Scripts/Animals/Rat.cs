public class Rat : Entity
{
    public override void HackingEnter(Player player)
    {
        _player = player;
        _player.InputComp.OnJumpEvent = Jump;
    }

    public override void HackingExit()
    {
        _player.InputComp.OnJumpEvent -= Jump;
        _player = null;
    }
}
