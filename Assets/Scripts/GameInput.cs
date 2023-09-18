using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;

    private PlayerInputActions _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerInput.Player.Enable();
        _playerInput.Player.Interact.performed += InteractOnPerformed;
        _playerInput.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
    }

    private void InteractAlternateOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return _playerInput.Player.Move.ReadValue<Vector2>().normalized;
    }
}
