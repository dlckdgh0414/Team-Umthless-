using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Entity
{
    public override void HackingEnter(Player player)
    {
        _player = player;
    }

    public override void HackingExit()
    {
        
    }
}
