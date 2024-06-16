using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private CharacterController characterController;

    [SerializeField] private float moveSpeed = 4.5f;

    #region Constant Variables
    private const float Gravity = -9.81f;
    private const float SpeedChangeRate = 10.0f;
    private const float SpeedOffset = 0.1f;
    private const float InputMagnitude = 1.0f;
    private const float RotationSmoothness = 0.1f;
    #endregion

    #region Cached Variables
    private float speed;
    private float targetRotation;
    private float rotationVelocity;
    #endregion

    private void Awake()
    {
        Instance = this;

        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMove();
        HandleRotate();
    }

    private void HandleMove()
    {
        float targetSpeed = moveSpeed;
        if (InputManager.Instance.Move == Vector2.zero) targetSpeed = 0.0f;

        AccelerateSpeed(targetSpeed);

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, Gravity, 0.0f) * Time.deltaTime);
    }

    private void HandleRotate()
    {
        if (InputManager.Instance.Move == Vector2.zero) return;

        Vector3 lookDirection = new Vector3(InputManager.Instance.Move.x, 0.0f, InputManager.Instance.Move.y).normalized;

        targetRotation = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, RotationSmoothness);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    private void AccelerateSpeed(float targetSpeed)
    {
        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

        if (currentHorizontalSpeed < targetSpeed - SpeedOffset || currentHorizontalSpeed > targetSpeed + SpeedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * InputMagnitude, Time.deltaTime * SpeedChangeRate);
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }
    }

    public float GetMoveAmount()
    {
        float horizontalInput = InputManager.Instance.Move.x;
        float verticalInput = InputManager.Instance.Move.y;

        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        return moveAmount;
    }
}