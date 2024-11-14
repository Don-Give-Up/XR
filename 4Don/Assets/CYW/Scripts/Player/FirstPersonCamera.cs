using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;                 // 플레이어의 Transform
    public float distanceFromTarget = 4f;    // 타겟과 카메라 사이의 거리
    public float height = 2f;                // 카메라의 높이
    public float rotationSmoothTime = 0.1f;  // 회전 부드럽게 하는 시간
    public float maxRotationSpeed = 100f;    // 회전 속도 한계

    private float verticalRotation;          // 수직 회전 값
    private float currentHorizontalRotation; // 현재 수평 회전 값
    private float targetHorizontalRotation;  // 목표 수평 회전 값
    private float rotationVelocity;          // 회전 속도

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        // 타겟 위치를 기준으로 카메라 위치 계산
        Vector3 targetPosition = Target.position + Vector3.up * height;

        // 목표 수평 회전 값을 타겟의 y 회전값으로 설정
        targetHorizontalRotation = Target.eulerAngles.y;

        // 목표 회전 각도에 자연스럽게 도달하도록 조정
        currentHorizontalRotation = Mathf.SmoothDampAngle(
            currentHorizontalRotation,
            targetHorizontalRotation,
            ref rotationVelocity,
            rotationSmoothTime,
            maxRotationSpeed
        );

        // 수직 및 수평 회전을 적용하여 카메라 위치 설정
        Quaternion rotation = Quaternion.Euler(verticalRotation, currentHorizontalRotation, 0);
        Vector3 direction = rotation * Vector3.back;
        transform.position = targetPosition + direction * distanceFromTarget;

        // 카메라가 항상 타겟을 바라보도록 회전
        transform.LookAt(targetPosition);
    }

    public void UpdateCameraRotation(float verticalInput)
    {
        // 수직 입력에 따라 카메라 각도를 조정
        float rotationSpeed = 50f;
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;

        // 수직 회전 제한
        verticalRotation = Mathf.Clamp(verticalRotation, -30f, 30f);
    }
    
}