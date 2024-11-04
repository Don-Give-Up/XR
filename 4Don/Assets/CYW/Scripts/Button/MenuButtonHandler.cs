using UnityEngine;
using UnityEngine.UI;


public class MenuButtonHandler : MonoBehaviour
{
    public MenuButtonController menuButtonController;

    public Button menuButton; // 처음 누를 버튼

    public Image menuBackground; // 이미지
    public Button moneyButton; // 첫 번째 버튼
    public Button newsButton; // 두 번째 버튼
    public Button planButton; // 세 번째 버튼

    public GameObject inventory;

    private bool isMenuActive = false; // 메뉴 활성화 상태

    private void Start()
    {
        // 메뉴 버튼 클릭 이벤트에 메소드 추가
        menuButton.onClick.AddListener(OnMenuButtonClick);

        // 이미지와 버튼 비활성화
        menuBackground.gameObject.SetActive(false);
        moneyButton.gameObject.SetActive(false);
        newsButton.gameObject.SetActive(false);
        planButton.gameObject.SetActive(false);
    }

    public void OnMenuButtonClick() // 메뉴 한 번 더 누르면 밑에 애들 꺼짐
    {
        isMenuActive = !isMenuActive; // 상태 반전

        // 상태에 따라 메뉴 활성화/비활성화
        ButtonsOn(isMenuActive);

        // 머니버튼 뉴스버튼 플랜버튼을 통해 뜬 것들도 꺼버려야 됨 아 왜 안 되
        if (!isMenuActive)
        {
            menuButtonController.object1.SetActive(false);
            menuButtonController.object2.SetActive(false);
            menuButtonController.object3.SetActive(false);
        }

        if (inventory.activeSelf)
            inventory.SetActive(false);
    }

    public void ButtonsOn(bool active)
    {
        // 상태에 따라 메뉴 활성화/비활성화
        menuBackground.gameObject.SetActive(active);
        moneyButton.gameObject.SetActive(active);
        newsButton.gameObject.SetActive(active);
        planButton.gameObject.SetActive(active);
    }

    public void SetMenuActive(bool active)
    {
        isMenuActive = active;
    }
}