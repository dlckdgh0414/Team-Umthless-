using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private Entity _entity;

    [Header("Setting")]
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private Vector2 wallRuningCheckSize;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform wallRuningPoint;

    public bool isRightWall;

    public void Initialized(Entity entity)
    {
        _entity = entity;
    }

    public bool IsWallCheck()
    {
        Collider2D wall = Physics2D.OverlapBox(transform.position, wallCheckSize, 0, whatIsWall);

        if (wall == null) return false;

        float dir = wall.transform.position.x - _entity.transform.position.x;
        isRightWall = Mathf.Sign(dir) > 0;

        return wall != null;
    }

    public bool IsWallRuningCheck()
    {
        Collider2D wall = Physics2D.OverlapBox(transform.position, wallRuningCheckSize, 0, whatIsWall);

        return wall != null;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, wallCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(wallRuningPoint.transform.position, wallRuningCheckSize);
        Gizmos.color = Color.white;
    }

#endif
}
