using UnityEngine;

public class Elephant : Entity
{

    [SerializeField] private AnimTypeSO _moveType;
    [SerializeField] private AnimTypeSO _pushType;

    [SerializeField] private float _pushPower;
    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    protected override void Move(Vector2 dir)
    {
        base.Move(dir);
        if (dir.x != 0)
        {
            AnimCompo.SetParam(_moveType, true);
        }
        else
        {
            AnimCompo.SetParam(_moveType, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {

            AnimCompo.SetParam(_pushType, true);

            if (collision.gameObject.TryGetComponent(out Rigidbody2D rigid))
            {
                rigid.constraints = RigidbodyConstraints2D.None;
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {

            AnimCompo.SetParam(_pushType, false);

            if (collision.gameObject.TryGetComponent(out Rigidbody2D rigid))
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public override void HackingExit()
    {
        _canMove = false;
        _player = null;
    }
}
