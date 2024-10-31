using System;
using UnityEngine;
using Cinemachine;

public class Test : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    
    [SerializeField] private Transform _target;
    
    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _virtualCamera.Follow = _target;
        }
    }
}
