using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float airMultiplier = 0.3f;
    
    [Header("GroundCheck")]
    [SerializeField] private float groundCheckRadius = 1f;
    [SerializeField] private float slopeCheckDistance = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Additional Settings")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    [Header("Key Binds")] 
    [SerializeField] private KeyBinds keys;
    
    private bool _isGrounded;
    private float _movementMultiplier;
    private float _currentSpeed;
    
    private Rigidbody _rigidbody;
    private RaycastHit _slopeHit;
    
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    private Vector3 _slopeMoveDirection;
    
    private void Start()
    {
        _isGrounded = false;
        _currentSpeed = playerSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();
        ReadInput();
        
        _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal);

        ControlSprint();
        
        if (_isGrounded)
        {
            if (Input.GetKeyDown(keys.jumpKey))
                Jump();
        }

        RigidbodyMove();
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void ReadInput()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        var transform1 = transform;
        _moveDirection = transform1.forward * _moveInput.y + transform1.right * _moveInput.x;
    }

    private void ControlSprint()
    {
        var targetSpeed = Input.GetKey(keys.sprintKey) ? sprintSpeed : playerSpeed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, acceleration * Time.deltaTime);
    }

    private void RigidbodyMove()
    {
        _movementMultiplier = Time.deltaTime * 100f;
        _rigidbody.drag = groundDrag;
        
        if (!_isGrounded)
        {
            _movementMultiplier *= airMultiplier;
            _rigidbody.drag = airDrag;
        }
        else if(OnSlope())
        {
            _moveDirection = _slopeMoveDirection;
        }
        
        _rigidbody.AddForce(_moveDirection.normalized * (_currentSpeed * _movementMultiplier), ForceMode.Acceleration);
    }

    private void Jump()
    {
        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
            
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out _slopeHit, slopeCheckDistance))
            return _slopeHit.normal != Vector3.up;

        return false;
    }
}
