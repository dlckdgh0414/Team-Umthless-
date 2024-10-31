using System;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader InputComp { get; private set; }

    private CinemachineVirtualCamera _virtualCamera;

    [Header("HackingSetting")]
    public float maxHackingCharge;
    public NotifyValue<float> _hackingCharging;
    private bool _isHacking;
    private Entity _currentEntity;
    private Entity _nextEntity;

    private void OnEnable()
    {
        InputComp.OnHackingChargingEvent += HandheldHacking;
    }

    private void Awake()
    {
        _hackingCharging = new NotifyValue<float>();
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        _hackingCharging.OnValueChanged += HandleHackingChanged;
    }

    private void OnDisable()
    {
        InputComp.OnHackingChargingEvent -= HandheldHacking;
    }
    
    private void HandheldHacking(bool isHacking)
    {
        _isHacking = isHacking;
        if (isHacking)
        {
            Vector2 point = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(point);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
            if (hit.collider.TryGetComponent(out Entity entity))
                _nextEntity = entity;
        }
    }

    private void Update()
    {
        if (_isHacking)
        {
            _hackingCharging.Value += Time.deltaTime;
        }
    }
    
    private void HandleHackingChanged(float prev, float next)
    {
        if (next > maxHackingCharge)
        {
            Hacking();
        }
    }

    private void Hacking()
    {
        //_currentEntity. //제유 오면 엔티티 수정후 해킹 나가기
        _currentEntity = _nextEntity;
        //_currentEntity //제유 오면 엔티티 수정후 해킹 들어가기
        _nextEntity = null;
        
        _virtualCamera.Follow = _currentEntity.transform;
    }
}
