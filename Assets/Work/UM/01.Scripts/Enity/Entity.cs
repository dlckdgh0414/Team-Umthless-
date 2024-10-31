using UnityEngine;

public abstract class Entity : MonoBehaviour, IHackingEnter, IHackingExit
{
    protected Rigidbody2D RigidCompo;
    protected AnimalsAnim AnimCompo;
    protected Player _player;

    protected EntityRenderer _renderer;
    public  GroundCheck CheckCompo { get; protected set; }

    [SerializeField]
    protected bool _canMove;

    protected virtual void Awake()
    {
        RigidCompo = GetComponent<Rigidbody2D>();
        AnimCompo = GetComponentInChildren<AnimalsAnim>();

        _canMove = false;

        _renderer = GetComponentInChildren<EntityRenderer>();
        CheckCompo = GetComponentInChildren<GroundCheck>();
    }

    [SerializeField] protected MovementDataSO _moveData;

    protected virtual void FixedUpdate()
    {
        if (!_canMove) return;
        Move(_player.InputComp.MoveDir);
    }

    protected virtual void Move(Vector2 dir)
    {
        RigidCompo.velocity = new Vector2(dir.x * _moveData.moveSpeed, RigidCompo.velocity.y);
        _renderer.FlipController(dir.x);
    }

    protected virtual void Jump()
    {
        RigidCompo.AddForce(Vector2.up * _moveData.jumpPower, ForceMode2D.Impulse);
    }

    public abstract void HackingEnter(Player player);

    public abstract void HackingExit();
}
