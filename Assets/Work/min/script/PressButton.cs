using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnPressedEvent?.Invoke();
    }

    private void OnButtonExit()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnButtonPressed();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
