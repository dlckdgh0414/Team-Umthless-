using UnityEngine;

public class GroundCheck : MonoBehaviour, IEntityComponent
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _whatIsGround;

    private Entity _entity;

    [field : SerializeField]
    public bool IsGround { get; private set; }
    public void CheckGround()
    {
        Collider2D groundCollider = Physics2D.OverlapBox(transform.position, _size, 0, _whatIsGround);
        IsGround = groundCollider;
    }

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
