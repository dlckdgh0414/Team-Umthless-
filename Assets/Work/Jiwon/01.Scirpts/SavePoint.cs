using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    public LayerMask whatIsTarget;
    private List<Transform> savePoints;
    private Transform _currentSavePoint;
    private Transform _nextSavePoint;
    private Entity _target;
    public Player _player;

    private void Awake()
    {
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out Point point))
            {
                point.Initialized(this);
                savePoints.Add(item);
            }
        }
    }

    public void ReSpawn()
    {
        _player.Hacking(_target);
    }

    public void SetTarget(Entity target)
    {
        _target = target;
    }

    public void SetSavePointe(Point point)
    {
        _currentSavePoint = point.transform;
    }
}
