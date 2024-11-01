using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [field: SerializeField] public InputReader InputComp { get; private set; }
    [SerializeField] private Entity initEntity;
    public CinemachineVirtualCamera VirtualCamera { get; private set; }

    public bool isSkill = false;

    [Header("HackingSetting")]
    public float maxHackingCharge;
    public UnityEvent OnHackingEvent;
    public NotifyValue<float> _hackingCharging;
    public float canHackingDistance;
    private bool _isHacking;
    public Entity currentEntity;
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
        currentEntity = initEntity;
        currentEntity.HackingEnter(this);
        VirtualCamera.Follow = currentEntity.transform;
        DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize, x => VirtualCamera.m_Lens.OrthographicSize = x, currentEntity._moveData.camFov, 1f);
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
                if (Vector3.Distance(currentEntity.transform.position, entity.transform.position) <=
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
            Hacking(_nextEntity);
        }
    }

    public void Hacking(Entity newEntity)
    {
        if (newEntity == null) return;
        if (currentEntity == newEntity) return;

        hackingUI.HackingCansle();

        _hackingCharging.Value = 0;
        _isHacking = false;

        currentEntity.HackingExit();
        currentEntity = newEntity;
        currentEntity.HackingEnter(this);
        DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize, x => VirtualCamera.m_Lens.OrthographicSize = x, currentEntity._moveData.camFov, 1f);
        _nextEntity = null;

        VirtualCamera.Follow = currentEntity.transform;
        OnHackingEvent?.Invoke();
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (currentEntity != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(currentEntity.transform.position, canHackingDistance);
            Gizmos.color = Color.white;
        }
    }

#endif
}
