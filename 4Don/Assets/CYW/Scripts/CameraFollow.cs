using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset; // 카메라와 플레이어 간의 오프셋

    private void LateUpdate()
    {
        // 플레이어 위치에 오프셋을 추가하여 카메라 위치 설정
        transform.position = player.position + offset;

        // 카메라가 플레이어의 방향을 바라보도록 회전
        transform.LookAt(player);
    }
}
