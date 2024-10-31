using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class Pressureplate : MonoBehaviour
{

    public UnityEvent OnPressEvent;

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
        transform.localScale = new Vector3(1, 0.5f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.localScale = new Vector3(1, 1);
        OnPressEvent.RemoveAllListeners();
    }
}
