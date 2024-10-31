using UnityEngine;

public class Elephant : Entity
{
    [SerializeField] private float _pushPower;
    public override void HackingEnter(Player player)
    {
        _player = player;
        _canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Push"))
        {
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
