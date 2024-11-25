using UnityEngine;
using UnityEngine.UI;

public class RotatingBlinkingLoader : MonoBehaviour
{
    private RectTransform rectTransform;  // 회전을 위한 RectTransform
    private CanvasGroup canvasGroup;  // 투명도를 위한 CanvasGroup
    public float rotationSpeed = 180f;  // 회전 속도
    public float blinkSpeed = 1f;  // 깜빡임 속도 (초)

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            // CanvasGroup이 없다면 추가
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        RotateSpinner();
        BlinkEffect();
    }

    // 회전 애니메이션
    private void RotateSpinner()
    {
        // RectTransform을 이용하여 회전
        rectTransform.Rotate(0, 0, -1 * rotationSpeed * Time.deltaTime);
    }

    // 깜빡임 효과
    private void BlinkEffect()
    {
        // 깜빡임을 위한 alpha 값 변경 (sin 함수 사용)
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        canvasGroup.alpha = alpha;
    }
}