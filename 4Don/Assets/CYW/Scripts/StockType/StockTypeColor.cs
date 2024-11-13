using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StockTypeColor: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   // public으로 8개의 Image를 선언합니다.
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;
    public Image image7;
    public Image image8;

    private Color[] originalColors;  // 각 이미지의 원래 색상(알파 값 포함)을 저장할 배열

    void Start()
    {
        // 원래 색상들을 배열에 저장
        originalColors = new Color[8];
        originalColors[0] = image1.color;
        originalColors[1] = image2.color;
        originalColors[2] = image3.color;
        originalColors[3] = image4.color;
        originalColors[4] = image5.color;
        originalColors[5] = image6.color;
        originalColors[6] = image7.color;
        originalColors[7] = image8.color;
    }

    void Update()
    {
        // 각 이미지에 대해 마우스 오버 여부를 확인하고 알파 값을 변경합니다.
        if (IsMouseOverImage(image1)) SetImageAlpha(image1, 96);
        else SetImageAlpha(image1, 0);

        if (IsMouseOverImage(image2)) SetImageAlpha(image2, 96);
        else SetImageAlpha(image2, 0);

        if (IsMouseOverImage(image3)) SetImageAlpha(image3, 96);
        else SetImageAlpha(image3, 0);

        if (IsMouseOverImage(image4)) SetImageAlpha(image4, 96);
        else SetImageAlpha(image4, 0);

        if (IsMouseOverImage(image5)) SetImageAlpha(image5, 96);
        else SetImageAlpha(image5, 0);

        if (IsMouseOverImage(image6)) SetImageAlpha(image6, 96);
        else SetImageAlpha(image6, 0);

        if (IsMouseOverImage(image7)) SetImageAlpha(image7, 96);
        else SetImageAlpha(image7, 0);

        if (IsMouseOverImage(image8)) SetImageAlpha(image8, 96);
        else SetImageAlpha(image8, 0);
    }

    // 마우스가 이미지 위에 올려졌는지 확인하는 함수
    private bool IsMouseOverImage(Image image)
    {
        RectTransform rectTransform = image.rectTransform;
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        // 이미지 영역 내에 마우스가 있을 경우
        return rectTransform.rect.Contains(localMousePosition);
    }

    // 이미지의 알파 값을 설정하는 함수
    private void SetImageAlpha(Image image, float alpha)
    {
        Color newColor = image.color;
        newColor.a = alpha / 255f;  // 0~1 사이로 알파 값 설정
        image.color = newColor;
    }

    // 마우스가 이미지 위로 들어왔을 때 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 알파 값 설정 (마우스 오버 시 96으로)
        SetImageAlpha(image1, 96);
        SetImageAlpha(image2, 96);
        SetImageAlpha(image3, 96);
        SetImageAlpha(image4, 96);
        SetImageAlpha(image5, 96);
        SetImageAlpha(image6, 96);
        SetImageAlpha(image7, 96);
        SetImageAlpha(image8, 96);
    }

    // 마우스가 이미지에서 벗어났을 때 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // 알파 값 초기화 (마우스 벗어났을 때 0으로)
        SetImageAlpha(image1, 0);
        SetImageAlpha(image2, 0);
        SetImageAlpha(image3, 0);
        SetImageAlpha(image4, 0);
        SetImageAlpha(image5, 0);
        SetImageAlpha(image6, 0);
        SetImageAlpha(image7, 0);
        SetImageAlpha(image8, 0);
    }
}
