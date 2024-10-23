using UnityEngine;

public class UIButtonHandler : MonoBehaviour
{
    
    public GameObject panel; // 패널을 할당할 변수
    private CanvasGroup canvasGroup;

    void Start()
    {
        // 패널을 비활성화합니다.
        canvasGroup = panel.GetComponent<CanvasGroup>();
        HidePanel();
    }

    public void OnButtonClick()
    {
        if (canvasGroup.alpha == 0) // 패널이 보이지 않는 경우
        {
            ShowPanel();
        }
        else // 패널이 보이는 경우
        {
            HidePanel();
        }
    }

    private void ShowPanel()
    {
        canvasGroup.alpha = 1; // 완전히 보이게
        canvasGroup.interactable = true; // 상호작용 가능
        canvasGroup.blocksRaycasts = true; // 레이캐스트 차단
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0; // 완전히 숨김
        canvasGroup.interactable = false; // 상호작용 불가능
        canvasGroup.blocksRaycasts = false; // 레이캐스트 차단 해제
    }
    
}

