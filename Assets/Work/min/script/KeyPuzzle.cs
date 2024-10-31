using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class KeyPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject _key;
    [SerializeField] private GameObject _keyHole;
    [SerializeField] private Transform _playerTrm;

    private bool _hasKey;

    public event Action OnKeyEnterEvent;

    private void Update()
    {
        if (_hasKey)
        {
            FollowPlayer();
            return;
        }

        CheckKeyInRange();
    }

    private void CheckKeyInRange()
    {
        if ((_playerTrm.position - _key.transform.position).magnitude < 2)
        {
            EarnKey();
        }
        else if ((_playerTrm.position - _keyHole.transform.position).magnitude < 2)
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
                OnKeyEnter();
        }
    }

    private void EarnKey()
    {
        _key.transform.DOScale(0.5f, 0.5f);
        _hasKey = true;
    }

    private void OnKeyEnter()
    {
        _hasKey = false;
        OnKeyEnterEvent?.Invoke();
    }

    private void FollowPlayer()
    {
        _key.transform.position = Vector3.Lerp(
                _key.transform.position, new Vector2(
                _playerTrm.position.x + 2,
                _playerTrm.position.y + 2), 0.05f);
    }
}
