using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
	private MainUI _textBoxOn;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _textBoxOn = FindObjectOfType<MainUI>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Animals"))
         _textBoxOn.isTextTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Anmals"))
        {
            _boxCollider.gameObject.SetActive(false);
            _textBoxOn.isTextTrigger = false;
        }
    }
}
