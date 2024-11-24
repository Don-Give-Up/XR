using UnityEngine;

public class LoadingSpinner : MonoBehaviour
{
    private RectTransform rectTransform;
    public float rotationSpeed = 180f; // 회전 속도 (도/초)

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        RotateSpinner();
    }

    void RotateSpinner()
    {
        // 이미지의 RectTransform을 이용하여 회전시킴
        rectTransform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }
}