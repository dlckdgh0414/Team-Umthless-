using DG.Tweening;
using UnityEngine;
using GGMPool;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _hasTime;
    [SerializeField] private float _time;
    [SerializeField] private PressButton _pressButton;

    private SpriteRenderer _doorSprite;
    private bool _isOpened;

    private void Awake()
    {
        _doorSprite = GetComponent<SpriteRenderer>();

        _pressButton.OnPressedEvent += HandleDoorChange;
    }

    public void HandleDoorChange(bool isOpened)
    {
        if (_isOpened == isOpened) return;

        _doorSprite.DOFade(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(isOpened);
        });

        _isOpened = isOpened;
    }
}
