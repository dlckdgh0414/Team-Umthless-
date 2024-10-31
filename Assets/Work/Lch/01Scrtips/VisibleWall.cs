using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class VisibleWall : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public NotifyValue<bool> IsVisible = new NotifyValue<bool>();

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        IsVisible.OnValueChanged += InvisibleingWall;
    }

    private void Update()
    {
        IsVisible.Value = false;
    }

    public void InvisibleingWall(bool prev, bool next)
    {
        if(!next)
        _sprite.DOFade(0, 0.01F);
        else
            _sprite.DOFade(1, 1.5f);
    }

    private void OnDisable()
    {
        IsVisible.OnValueChanged -= InvisibleingWall;
    }
}
