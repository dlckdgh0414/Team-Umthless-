using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lizard : Entity
{
    
    
    public override void HackingEnter(Player player)
    {
        _player = player;
        
    }

    public override void HackingExit()
    {
        
    }
}
