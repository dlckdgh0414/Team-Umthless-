using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
	[SerializeField] private Image _image;

    private void Start()
    {
        _image.gameObject.SetActive(false);
    }
}
