using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lconChange : MonoBehaviour
{
	private Image _iconMask;

    private void Awake()
    {
        _iconMask = GetComponentInChildren<Image>();
    }
}
