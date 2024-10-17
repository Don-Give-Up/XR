using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 5f; // 점프 힘
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D 또는 방향키 입력
        float moveVertical = Input.GetAxis("Vertical"); // W/S 또는 방향키 입력

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;
        rb.MovePosition(transform.position + movement * Time.deltaTime); // 이동
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // 바닥에 있을 때 스페이스바로 점프
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았는지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 바닥에서 떨어졌는지 확인
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
