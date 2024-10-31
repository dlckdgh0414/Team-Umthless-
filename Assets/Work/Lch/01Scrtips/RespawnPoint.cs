using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _respawnTarget;
    public UnityEvent OnSpawnPointActivated;

    private void Start()
    {
        OnSpawnPointActivated.AddListener(() => GetComponentInParent<RespawnManager>().UpdateRespawnPoint(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _respawnTarget = collision.gameObject;
            OnSpawnPointActivated?.Invoke();
        }
    }

    public void RespawnPlayer()
    {
        _respawnTarget.transform.position = transform.position;
    }

    public void DisableRespawnPoint()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void RestRespawnPoint()
    {
        _respawnTarget = null;
        GetComponent<Collider2D>().enabled = true;
    }
}
