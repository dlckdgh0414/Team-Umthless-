using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lconChange : MonoBehaviour
{
	private Image _iconMask;
    private Entity _player;

    private void Awake()
    {
        _player = FindObjectOfType<Entity>();
        _iconMask = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        _iconMask.sprite = _player._moveData.IconSprite;
    }
}
