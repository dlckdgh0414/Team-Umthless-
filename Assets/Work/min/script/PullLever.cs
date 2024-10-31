using System;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    private Transform _playerTrm;
    public event Action<bool> OnPressedEvent;
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _playerTrm = GameObject.FindWithTag("Player").transform;
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
        _animator.SetBool("IsPulled", isPressed);
        OnPressedEvent?.Invoke(isPressed);
    }
}
