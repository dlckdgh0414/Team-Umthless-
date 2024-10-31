using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Console;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerInputActions
{
    private Console _console;
    
    public Vector2 MoveDir { get; private set; }
    public Action OnJumpEvent;
    public Action<bool> OnJumpChargingEvent;
    public Action OnSkillEvent;
    public Action<bool> OnSkillChargingEvent;

    private void OnEnable()
    {
        if (_console == null)
        {
            _console = new Console();
            _console.Enable();
        }
        _console.PlayerInput.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movedir = context.ReadValue<Vector2>();
        MoveDir = movedir;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpEvent?.Invoke();
            OnJumpChargingEvent?.Invoke(true);
        }
        else if (context.canceled)
            OnJumpChargingEvent?.Invoke(false);
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSkillEvent?.Invoke();
            OnSkillChargingEvent?.Invoke(true);
        }
        else if (context.canceled)
            OnSkillChargingEvent?.Invoke(false);
    }
}
