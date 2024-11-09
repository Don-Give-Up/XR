
using Fusion;
using UnityEngine;

public class MJPlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;
    private NetworkCharacterController NoChDrop;

    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;
    public Animator anim;

    public FirstPersonCamera Camera;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (!HasStateAuthority)
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
        if (Mathf.Abs(moveVertical) > float.Epsilon || Mathf.Abs(moveHorizontal) > float.Epsilon) // 위아래 방향키 입력이 있을 때
        {
            anim.SetBool("IsWalk", true); // 걷기 애니메이션 시작
        }
        /*else if (Mathf.Abs(moveHorizontal) > float.Epsilon) // 좌우 방향키 입력이 있을 때
        {
            anim.SetBool("IsWalk", false); // 걷기 애니메이션 멈춤
        }*/
        else
        {
            anim.SetBool("IsWalk", false); // 입력이 없을 때 걷기 애니메이션 멈춤
        }
    }
    
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = FindAnyObjectByType<FirstPersonCamera>();
            Camera.Target = transform;
            
            NoChDrop = GetComponent<NetworkCharacterController>();
            NoChDrop.Teleport(new Vector3(226f, 47f, 365f));
        }
    }

    public override void FixedUpdateNetwork()
    {
        // Only move own player and not every other player. Each player controls its own player object.
        if (HasStateAuthority == false)
        {
            return;
        }

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Runner.DeltaTime * PlayerSpeed;

        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _jumpPressed = false;
    }
    
}