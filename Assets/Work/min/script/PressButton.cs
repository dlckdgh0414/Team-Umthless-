using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PressButton : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    public event Action<bool> OnPressedEvent;
    private Animator _animator;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckButtonPress();
    }

    private void CheckButtonPress()
    {
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
        //_animator.SetBool("Pressed", isPressed);
        OnPressedEvent?.Invoke(isPressed);
    }
}
