using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;

    private float verticalRotation;
    private float horizontalRotation;
    public float distanceFromTarget = 5f; // 타겟과 카메라 사이의 거리
    public float height = 2f; // 카메라의 높이

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        // 카메라의 위치 계산
        Vector3 targetPosition = Target.position + Vector3.up * height; // 타겟의 높이에 따라 카메라 높이 조정
        Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        Vector3 direction = rotation * Vector3.back; // 카메라가 타겟을 바라보도록 방향 설정
        transform.position = targetPosition + direction * distanceFromTarget;

        // 카메라 회전 업데이트
        transform.rotation = rotation;
    }

    public void UpdateCameraRotation(float horizontalInput)
    {
        // 좌우 방향키에 따른 회전 업데이트
        float rotationSpeed = 100f; // 회전 속도
        horizontalRotation += horizontalInput * rotationSpeed * Time.deltaTime;

        // 수직 회전 제한 (옵션)
        verticalRotation = Mathf.Clamp(verticalRotation, -30f, 30f);
    }

}
