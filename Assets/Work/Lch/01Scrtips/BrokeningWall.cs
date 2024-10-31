using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BrokeningWall : MonoBehaviour
{
    public UnityEvent OnGrithchEvent;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
           BrokeningObj();
    }

    public void BrokeningObj()
    {
       _sprite.material.DOFade(0, 1.5f)
            .OnComplete(() => Destroy(gameObject));
        
        OnGrithchEvent?.Invoke();
    }
}
