using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset; // 카메라와 플레이어 사이의 오프셋
    public float rotationSpeed = 2f; // 회전 속도 (더 낮은 값으로 설정)

    void Start()
    {
        // 초기 오프셋 설정
        offset = new Vector3(0, 2, -5);
    }

    void LateUpdate()
    {
        // 카메라의 위치를 플레이어 위치에 오프셋을 더해 설정
        transform.position = player.position + offset;

        // 플레이어의 회전을 부드럽게 따라가기
        Quaternion targetRotation = Quaternion.Euler(0, player.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
