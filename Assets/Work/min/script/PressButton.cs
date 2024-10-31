using System;
using UnityEngine;
using DG.Tweening;

public class PressButton : MonoBehaviour
{
    public event Action OnPressedEvent;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnButtonPressed()
    {
        _animator.SetBool("Pressed", true);
        OnPressedEvent?.Invoke();
    }

    private void OnButtonExit()
    {
        _animator.SetBool("Pressed", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnButtonPressed();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnButtonExit();
    }
}
