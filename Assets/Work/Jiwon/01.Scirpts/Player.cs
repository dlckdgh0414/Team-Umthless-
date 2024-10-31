using System;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader InputComp { get; private set; }
    [SerializeField] private Entity initEntity;
    public CinemachineVirtualCamera VirtualCamera { get; private set; }

    public bool isSkill = false;
    
    [Header("HackingSetting")]
    public float maxHackingCharge;
    public UnityEvent OnHackingEvent;
    public NotifyValue<float> _hackingCharging;
    public float canHackingDistance;
    private bool _isHacking;
    private Entity _currentEntity;
    private Entity _nextEntity;
    [SerializeField] private HackingUI hackingUI;

    private void OnEnable()
    {
        InputComp.OnHackingChargingEvent += HandheldHacking;
    }

    private void Awake()
    {
        _hackingCharging = new NotifyValue<float>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();

        _hackingCharging.OnValueChanged += HandleHackingChanged;

    }

    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        _currentEntity = initEntity;
        _currentEntity.HackingEnter(this);
        VirtualCamera.Follow = _currentEntity.transform;
        DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize, x => VirtualCamera.m_Lens.OrthographicSize = x, _currentEntity._moveData.camFov, 1f);
    }

    private void OnDisable()
    {
        InputComp.OnHackingChargingEvent -= HandheldHacking;
    }
    
    private void HandheldHacking(bool isHacking)
    {
        if (isHacking)
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector3.forward);
            
            if (!hit) return;

            if (hit.collider.TryGetComponent(out Entity entity))
            {
                if (Vector3.Distance(_currentEntity.transform.position, entity.transform.position) <=
                    canHackingDistance)
                {
                    _nextEntity = entity;
                    _isHacking = true;
                }
            }
        }
        else if (!isHacking && _hackingCharging.Value < maxHackingCharge)
        {
            _isHacking = false;
            
            _hackingCharging.Value = 0;
            _nextEntity = null;
            hackingUI.HackingCansle();
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 point = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(point);
    }

    private void Update()
    {
        if (_isHacking)
        {
            _hackingCharging.Value += Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector3.forward);
            if (!hit)
            {
                _isHacking = false;
            
                _hackingCharging.Value = 0;
                _nextEntity = null;
                hackingUI.HackingCansle();
                return;
            }

            if (!hit.collider.TryGetComponent(out Entity entity))
            {
                _isHacking = false;
            
                _hackingCharging.Value = 0;
                hackingUI.HackingCansle();
                _nextEntity = null;
            }
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
        if (_nextEntity == null) return;
        if (_currentEntity == _nextEntity) return;
        
        hackingUI.HackingCansle();
        
        _hackingCharging.Value = 0;
        _isHacking = false;
        
        _currentEntity.HackingExit();
        _currentEntity = _nextEntity;
        _currentEntity.HackingEnter(this);
        DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize, x => VirtualCamera.m_Lens.OrthographicSize = x, _currentEntity._moveData.camFov, 1f);
        _nextEntity = null;
        
        VirtualCamera.Follow = _currentEntity.transform;
        OnHackingEvent?.Invoke();
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (_currentEntity != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_currentEntity.transform.position, canHackingDistance);
            Gizmos.color = Color.white;
        }
    }

#endif
}
