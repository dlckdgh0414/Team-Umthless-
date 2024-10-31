using DG.Tweening;
using UnityEngine;

public class VisibleWall : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public NotifyValue<bool> IsVisible = new NotifyValue<bool>();

    [SerializeField] private float _radius;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        IsVisible.OnValueChanged += InvisibleingWall;
    }

    public void InvisibleingWall(bool prev, bool next)
    {
        if (next)
            _sprite.DOFade(1, 1.5f);
        else
            _sprite.DOFade(0, 1.5F);
    }

    private void OnDisable()
    {
        IsVisible.OnValueChanged -= InvisibleingWall;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
