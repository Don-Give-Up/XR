using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        // 메인 카메라의 Transform을 가져옴
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // 객체가 항상 카메라를 바라보도록 회전
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}