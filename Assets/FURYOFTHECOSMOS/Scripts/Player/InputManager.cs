using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
        
    private PlayerInputActions inputActions;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    private void Awake()
    {
        Instance = this;

        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Look.performed += OnLook;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }
    
    public void DisableMovement()
    {
        Debug.Log("Disabled");
        Move = Vector2.zero;
        Look = Vector2.zero;
        inputActions.Player.Look.Disable();
        inputActions.Player.Move.Disable();
    }

    public void EnableMovement()
    {
        Debug.Log("Enabled");
        inputActions.Player.Look.Enable();
        inputActions.Player.Move.Enable();
    }
}