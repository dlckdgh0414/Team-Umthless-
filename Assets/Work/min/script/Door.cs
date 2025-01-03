using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent DoorChangeEvent;

    [SerializeField] private bool _hasTime;
    [SerializeField] private float _currentTime, _time;
    [SerializeField] private PullLever _pressButton;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _doorSprite;

    private bool _isOpened;
    private bool _isTimerStart;

    private void Awake()
    {
        _doorSprite = transform.Find("Visual").GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        if (_pressButton != null)
            _pressButton.OnPressedEvent += HandleDoorChange;
    }

    private void Update()
    {
        DoorTimer();
    }

    private void DoorTimer()
    {
        if (_isTimerStart)
        {
            Debug.Log("sdf");
            _currentTime += Time.deltaTime;

            if (_currentTime > _time)
            {
                OnTimerEnd();
            }
        }
    }

    private void OnTimerEnd()
    {
        _currentTime = 0;
        _isTimerStart = false;

        if (_pressButton != null)
            _pressButton.ButtonStatus(false);

        DOVirtual.DelayedCall(0.25f, () =>
        _doorSprite.DOFade(1, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(true);
            _boxCollider.enabled = true;
            _isOpened = false;
        }));

        DoorChangeEvent?.Invoke();
    }

    public void HandleDoorChange(bool isOpened)
    {
        if (_isOpened == isOpened) return;

        DOVirtual.DelayedCall(0.25f, () =>
        _doorSprite.DOFade(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(isOpened);
            _boxCollider.enabled = !isOpened;
        }));

        if (!_isOpened && _hasTime)
            _isTimerStart = true;

        _isOpened = isOpened;
        DoorChangeEvent?.Invoke();
    }
}
