using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class VisibleWall : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public UnityEvent OnVisibleWall;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _sprite.material.DOFade(0, 0.01F);
    }

    public void VisibleingWall()
    {
        _sprite.material.DOFade(1, 1.5f);
    }
}
