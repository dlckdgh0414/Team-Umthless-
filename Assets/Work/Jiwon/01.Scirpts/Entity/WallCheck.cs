using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WallCheck : MonoBehaviour
{
    private Entity _entity;
    
    [Header("Setting")]
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private LayerMask whatIsWall;

    public bool isRightWall;

    public void Initialized(Entity entity)
    {
        _entity = entity;
    }

    public bool IsWallCheck()
    {
        Collider2D wall = Physics2D.OverlapBox(transform.position, wallCheckSize, 0, whatIsWall);
        
        float dir = wall.transform.position.x - _entity.transform.position.x;
        isRightWall = Mathf.Sign(dir) > 0;
        
        return wall != null;
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
