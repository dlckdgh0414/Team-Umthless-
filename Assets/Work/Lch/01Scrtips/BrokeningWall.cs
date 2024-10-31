using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeningWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BrokeningObj();
    }

    private void BrokeningObj()
    {
        
    }
}
