using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
	private RespawnPoint _currentPoint;

    [SerializeField] private List<RespawnPoint> respawnPoint;

    private void Awake()
    {
        foreach(Transform item in transform)
        {
            respawnPoint.Add(item.GetComponent<RespawnPoint>());
        }

        _currentPoint = respawnPoint[0];
    }

    public void UpdateRespawnPoint(RespawnPoint newPoint)
    {
        _currentPoint.DisableRespawnPoint();
        _currentPoint = newPoint;
    }

    public void Respawn(GameObject respawnTarget)
    {
        _currentPoint.RespawnPlayer();
        respawnTarget.SetActive(true);
    }
}
