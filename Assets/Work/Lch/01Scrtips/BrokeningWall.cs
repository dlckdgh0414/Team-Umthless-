using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BrokeningWall : MonoBehaviour
{
    public UnityEvent OnGrithchEvent;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<Rhino>(out Rhino rhino))
            {
                if (rhino._isDashing)
                {
                    rhino.ResetVelocity();
                    BrokeningObj();
                }
            }
        }
    }

    public void BrokeningObj()
    {
        _sprite.DOFade(0, 1.5f);

        OnGrithchEvent?.Invoke();
    }
}
