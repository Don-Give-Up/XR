using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;

    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;
    
    public Camera Camera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        // Only move own player and not every other player. Each player controls its own player object.
        if (HasStateAuthority == false)
        {
            return;
        }
        
        // Y 축의 로테이션 값이 90도

        // Initialize vertical velocity
        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        // Move player forward/backward with up/down arrow keys
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * verticalInput * Runner.DeltaTime * PlayerSpeed;

        // Gravity and jumping logic
        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);

        // Rotate player based on horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            // Rotate player based on horizontal input
            float rotationSpeed = 100f; // 회전 속도 조절
            float rotationAmount = horizontalInput * rotationSpeed * Runner.DeltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }

        _jumpPressed = false;

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }
    }

    
    
    
}