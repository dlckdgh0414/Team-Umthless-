using System;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private LayerMask whatIsWall;

    public bool IsWallCheck()
    {
        bool isWall = Physics2D.OverlapBox(transform.position, wallCheckSize, 0, whatIsWall);
        return isWall;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, wallCheckSize);
        Gizmos.color = Color.white;
    }

#endif
}
