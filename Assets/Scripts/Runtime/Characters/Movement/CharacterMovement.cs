using UnityEngine;
using Zenject;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;
    private IMovementStrategy _movementStrategy;
    private Camera _characterCamera;

    private float _yaw = 0f;
    private float _pitch = 0f;
    private float _verticalVelocity = 0f;
    private bool _isGrounded;

    private CharacterData _data;
    private bool _isPaused = false;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string MOUSEX = "Mouse X";
    private const string MOUSEY = "Mouse Y";
    private TimescaleManager _timescaleManager;
    [Inject]
    public void Construct(IMovementStrategy initialStrategy, TimescaleManager timescaleManager)
    {
        _timescaleManager = timescaleManager;
        _movementStrategy = initialStrategy;
    }

    void OnEnable()
    {
        _timescaleManager.OnPauseGame += PauseGame;
        _timescaleManager.OnResumeGame += ResumeGame;
    }

    void OnDisable()
    {
        _timescaleManager.OnPauseGame -= PauseGame;
        _timescaleManager.OnResumeGame -= ResumeGame;
    }



    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _characterCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (_isPaused) return;
        _isGrounded = _controller.isGrounded;
        Movement();
        JumpAndGravity();
        SetStrategy();
        LookAt();
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HORIZONTAL), 0, Input.GetAxisRaw(VERTICAL)).normalized;

        Vector3 moveDirection = Vector3.zero;
        if (input.magnitude > 0)
        {
            moveDirection = _characterCamera.transform.TransformDirection(input);
            moveDirection.y = 0;
            moveDirection = moveDirection.normalized;
        }

        _movementStrategy.Move(_controller, _data, moveDirection, Time.deltaTime);
    }

    private void JumpAndGravity()
    {
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _verticalVelocity = _data.jumpForce;
        }

        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        _controller.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
    }

    private void LookAt()
    {
        _yaw += Input.GetAxis(MOUSEX) * _data.HorizontalLookSensitivity;
        _pitch -= Input.GetAxis(MOUSEY) * _data.VerticalLookSensitivity;
        _pitch = Mathf.Clamp(_pitch, _data.VerticalLookLimits.x, _data.VerticalLookLimits.y);

        transform.eulerAngles = new Vector3(0, _yaw, 0);
        _characterCamera.transform.localEulerAngles = new Vector3(_pitch, 0, 0);
    }

    private void SetStrategy()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _movementStrategy = new SprintStrategy();
        }
        else
        {
            _movementStrategy = new WalkStrategy();
        }
    }

    public void SetCharacterData(CharacterData data)
    {
        _data = data;
    }

    private void PauseGame() => _isPaused = true;
    private void ResumeGame() => _isPaused = false;
}
