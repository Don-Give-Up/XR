
using System;
using System.Linq;
using Fusion;
using Fusion.Addons.SimpleKCC;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MJPlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;

    private SimpleKCC _controller;

    public float PlayerSpeed = 2f;

    public float JumpForce = 5f;
    public float GravityValue = -9.81f;
    public Animator anim;


    public AudioSource audioSource;

    public AudioClip stepSound1;
    public AudioClip stepSound2;

    private bool isFirstsound = true;
    private bool isTeleported = false;

    private int _spawnCount;
    private Vector3 _teleportPosition;
    private PlayerCamera playerCamera;

    private string[] nicknames = new string[7] { "박민주", "박유진", "조여원", "정보영", "박민정", "김채호", "송호진" };
    public TMP_Text nick;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
            PlayStepSound();
        }
        else
        {
            anim.SetBool("IsWalk", false); // 입력이 없을 때 걷기 애니메이션 멈춤
        }

        
    }
    
    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;
        
        _controller = GetComponent<SimpleKCC>();
            
        _spawnCount = PhoStartGame.Instance.runner.ActivePlayers.Count();
        Debug.Log(_spawnCount);
            
        /*if (SceneManager.GetActiveScene().name == "3DWork 1")
            {
                BEQuiz.Instance.QuizTeleport += Teleport;
                Teleport();
            }*/

        int playerOrder = Runner.SessionInfo.PlayerCount - 1; 
        
        RPCNickName(playerOrder);
        playerCamera = PlayerCamera.Instance;
        playerCamera.SetTarget(transform);
                
        Spuare();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPCNickName(int playerOrder)
    {
        if (playerOrder >= 0 && playerOrder < nicknames.Length)
        {
            nick.text = nicknames[playerOrder]; 
        }
        else
        {
            
        }
    }
    

    public void Spuare()
    {
        if (_controller == null)
        {
            _controller = GetComponent<SimpleKCC>();
        }
    
        if (_controller != null)
        {
            Debug.Log("컨트롤러있음?", _controller);
            Teleport(new Vector3(225f + _spawnCount, 46f, 362f), false);
        }
        else
        {
            Debug.LogError("SimpleKCC controller not found!");
        }
    }


    public void Teleport(Vector3 pos, bool useSpawnPos)
    {
        isTeleported = true;
        
        if (useSpawnPos)
            pos.z += _spawnCount;
        
        _teleportPosition = pos;
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        if (_controller.IsGrounded)
        {
            _velocity = new Vector3(0, -1f, 0);
        }
    
        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Runner.DeltaTime * PlayerSpeed;
    
        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.IsGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);

        // move 벡터가 zero가 아닐 때만 회전 적용
        if (move != Vector3.zero)
        {
            _controller.SetLookRotation(Quaternion.LookRotation(move));
        }

        _jumpPressed = false;

        if (isTeleported)
        {
            isTeleported = false;
            _controller.SetPosition(_teleportPosition, true, true);
        }
    }

    // 따발총 소리 해결
    private void PlayStepSound()
    {
        // 소리가 재생 중일 때는 새로운 소리를 재생하지 않음
        if (audioSource.isPlaying)
        {
            return; // 소리가 재생 중이면 아무 것도 하지 않음
        }

        if (isFirstsound)
        {
            audioSource.PlayOneShot(stepSound1); // 첫 번째 발소리 재생
        }
        else
        {
            audioSource.PlayOneShot(stepSound2); // 두 번째 발소리 재생
        }

        // 발소리 번갈아 재생
        isFirstsound = !isFirstsound;
    }
   
}