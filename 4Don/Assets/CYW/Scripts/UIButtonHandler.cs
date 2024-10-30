using UnityEngine;

public class UIButtonHandler : MonoBehaviour
{
    public GameObject panel; // 패널을 할당할 변수

    void Start()
    {
        HidePanel();
    }

    public void OnButtonClick()
    {
        if (!panel.activeSelf) // 패널이 보이지 않는 경우
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
        panel.SetActive(true);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
    }
    
    
    
    
}

