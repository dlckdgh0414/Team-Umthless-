using UnityEngine;
using UnityEngine.InputSystem;

public class ESC : MonoBehaviour
{
    [SerializeField] GameObject _escUI;
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            _escUI.SetActive(true);
    }
}
