using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public static FirstPersonCamera Instance { get; private set; }
    public Transform Target;
    public float distanceFromTarget = 4f;
    public float height = 2f;
    public float rotationSmoothTime = 0.1f;
    public float maxRotationSpeed = 100f;
    
    [SerializeField] private Camera personalCamera;  // 개인용 카메라
    [SerializeField] private Camera fixedCamera;     // 고정 카메라
    
    private float verticalRotation;
    private float currentHorizontalRotation;
    private float targetHorizontalRotation;
    private float rotationVelocity;
    private bool useFixedCamera = false;

    private void Awake()
    {
        Instance = this;
        fixedCamera.enabled = false;  // 시작시 고정 카메라는 비활성화
    }

    public void SwitchToFixedCamera()
    {
        useFixedCamera = true;
        personalCamera.enabled = false;
        fixedCamera.enabled = true;
    }
    
    public void SwitchToPersonalCamera()
    {
        useFixedCamera = false;
        personalCamera.enabled = true;
        fixedCamera.enabled = false;
    }

    void LateUpdate()
    {
        if (useFixedCamera) return;  // 고정 카메라 사용 중이면 개인 카메라 업데이트 중지
        
        if (Target == null) return;

        // 기존 카메라 로직
        Vector3 targetPosition = Target.position + Vector3.up * height;
        targetHorizontalRotation = Target.eulerAngles.y;
        
        currentHorizontalRotation = Mathf.SmoothDampAngle(
            currentHorizontalRotation,
            targetHorizontalRotation,
            ref rotationVelocity,
            rotationSmoothTime,
            maxRotationSpeed
        );

        Quaternion rotation = Quaternion.Euler(verticalRotation, currentHorizontalRotation, 0);
        Vector3 direction = rotation * Vector3.back;
        transform.position = targetPosition + direction * distanceFromTarget;
        transform.LookAt(targetPosition);
    }

    public void UpdateCameraRotation(float verticalInput)
    {
        if (useFixedCamera) return;  // 고정 카메라 사용 중이면 회전 업데이트 중지
        
        float rotationSpeed = 50f;
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -30f, 30f);
    }
}