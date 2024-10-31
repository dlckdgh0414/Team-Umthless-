using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HackingUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Image _gayg;
    private Image _hacking;

    private void Awake()
    {
        _gayg = transform.GetChild(0).GetComponent<Image>();
        _hacking = GetComponent<Image>();
        _hacking.enabled = false;
        _gayg.enabled = false;
    }

    private void Start()
    {
        _player._hackingCharging.OnValueChanged += HandleHackingValue;
        HackingCansle();
    }

    private void HandleHackingValue(float prev, float next)
    {
        if (next > 0)
        {
            _hacking.enabled = true;
            _gayg.enabled = true;

            transform.position = Mouse.current.position.ReadValue();
            _gayg.fillAmount = next / 2;
        }
    }

    public void HackingCansle()
    {
        _hacking.enabled = false;
        _gayg.enabled = false;
        _gayg.fillAmount = 0;
    }

    private void OnDisable()
    {
        _player._hackingCharging.OnValueChanged -= HandleHackingValue;
    }
}
