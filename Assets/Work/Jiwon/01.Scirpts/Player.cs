using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader InputComp { get; private set; }
    [SerializeField] private Entity initEntity;
    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    
    [Header("HackingSetting")]
    public float maxHackingCharge;
    public UnityEvent OnHackingEvent;
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
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();

        _hackingCharging.OnValueChanged += HandleHackingChanged;

        Initialized();
    }

    public void Initialized()
    {
        _currentEntity = initEntity;
        _currentEntity.HackingEnter(this);
        VirtualCamera.Follow = _currentEntity.transform;
    }

    private void OnDisable()
    {
        InputComp.OnHackingChargingEvent -= HandheldHacking;
    }
    
    private void HandheldHacking(bool isHacking)
    {
        if (isHacking)
        {
            _isHacking = true;
            
            Vector2 point = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(point);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
            if (hit.collider.TryGetComponent(out Entity entity))
                _nextEntity = entity;
        }
        else if (!isHacking)
        {
            _isHacking = false;
            
            _hackingCharging.Value = 0;
            _currentEntity = null;
        }
    }

    private void Update()
    {
        if (_isHacking)
            _hackingCharging.Value += Time.deltaTime;
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
        
        _hackingCharging.Value = 0;
        _isHacking = false;
        
        _currentEntity.HackingExit();
        _currentEntity = _nextEntity;
        _currentEntity.HackingEnter(this);
        _nextEntity = null;
        
        VirtualCamera.Follow = _currentEntity.transform;
        OnHackingEvent?.Invoke();
    }
}
