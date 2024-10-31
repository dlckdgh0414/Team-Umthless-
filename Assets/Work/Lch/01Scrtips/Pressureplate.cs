using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class Pressureplate : MonoBehaviour
{
    public UnityEvent OnPressEvent;
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.parent.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PressPlate();
        }
    }

    private void PressPlate()
    {
        OnPressEvent?.Invoke();
        _animator.SetBool("IsPressed", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnPressEvent.RemoveAllListeners();
        _animator.SetBool("IsPressed", false);
    }
}
