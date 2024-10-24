

using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    // 조절 가능한 변수들
    public float amplitude = 0.5f;  // 진폭, 오브젝트가 얼마나 크게 움직일지
    public float frequency = 1f;    // 주파수, 움직이는 속도

    // 시작 위치 저장
    private Vector3 startPosition;

    void Start()
    {
        // 오브젝트의 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 시간에 따른 사인파 생성
        float newY = Mathf.Sin(Time.time * frequency) * amplitude;

        // 오브젝트의 새로운 위치 설정 (Y축만 변동)
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);
    }
}