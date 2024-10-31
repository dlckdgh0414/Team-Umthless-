using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PullLever : MonoBehaviour
{
    private Transform _playerTrm;
    public event Action<bool> OnPressedEvent;
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }

    private void Update()
    {
        CheckButtonPress();
    }

    private void CheckButtonPress()
    {
        if (_playerTrm == null) return;

        if ((_playerTrm.position - transform.position).magnitude < 1)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ButtonStatus(true);
            }
        }
    }

    public void ButtonStatus(bool isPressed)
    {
        _animator.SetBool("IsPulled", isPressed);
        OnPressedEvent?.Invoke(isPressed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal"))
        {
            _playerTrm = collision.transform;
        }
    }
}
