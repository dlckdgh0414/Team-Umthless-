using UnityEngine;
using UnityEngine.Events;

public class PlayerMassCheck : MonoBehaviour
{
    public UnityEvent OnBrokenEvent;

    private Rigidbody2D _rbCompo;

    private void Awake()
    {
        _rbCompo = GetComponentInParent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody2D collisionRbcom))
            {
                if (collisionRbcom.mass > _rbCompo.mass)
                {
                    OnBrokenEvent?.Invoke();
                }
            }
        }
    }
}
