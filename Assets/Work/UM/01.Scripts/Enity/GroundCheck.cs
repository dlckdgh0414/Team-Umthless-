using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _whatIsGround;

    public bool IsGround { get; private set; }

    public void CheckGround()
    {
        Collider2D groundCollider = Physics2D.OverlapBox(transform.position, _size, 0, _whatIsGround);
        IsGround = groundCollider;
    }
}
