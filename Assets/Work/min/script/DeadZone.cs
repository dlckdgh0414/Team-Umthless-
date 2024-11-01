using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private SavePoint _savePoint;

    private void Awake()
    {
        _savePoint = GameObject.Find("SavePoint").GetComponent<SavePoint>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Animal"))
        {
            _savePoint.ReSpawn();
        }
    }
}
