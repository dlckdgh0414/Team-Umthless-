using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        savePoints = new List<Transform>();

        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out Point point))
            {
                Debug.Log(savePoints);
                point.Initialized(this);
                savePoints.Add(item);
            }
        }

        _player.InputComp.OnRespawnEvent += ReSpawn;
    }

    private void OnDisable()
    {
        _player.InputComp.OnRespawnEvent -= ReSpawn;
    }

    public void ReSpawn()
    {
        SceneManager.LoadScene(1);
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
