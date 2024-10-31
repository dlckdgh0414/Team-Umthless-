using UnityEngine;

public class Spider : Entity
{
    private SpringJoint2D _joint;
    private LineRenderer _line;

    private bool _isSwing;

    protected override void Awake()
    {
        base.Awake();
        _joint = GetComponent<SpringJoint2D>();
        _line = GetComponent<LineRenderer>();
        
        _joint.enabled = false;
    }

    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
        _player.InputComp.OnSkillEvent += HandleSwingEvent;
    }

    private void HandleSwingEvent()
    {
        if (!_isSwing)
        {
            _isSwing = true;
            _joint.enabled = true;
            
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
    }
}
