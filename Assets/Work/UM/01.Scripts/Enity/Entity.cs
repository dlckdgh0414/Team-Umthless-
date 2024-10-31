using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Rigidbody2D RigidCompo;
    protected GroundCheck GroundCheckCompo;

    private void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        GroundCheckCompo = GetComponentInChildren<GroundCheck>(); 
    }

    [SerializeField] protected MovementDataSO _moveData;

    protected virtual void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, RigidCompo.velocity.y);
    }

    protected virtual void Jump()
    {
        RigidCompo.AddForce(Vector2.up * _moveData.jumpPower, ForceMode2D.Impulse);
    }
}
