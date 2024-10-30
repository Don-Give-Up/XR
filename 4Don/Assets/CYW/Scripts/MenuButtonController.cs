using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public Button menuButton;

    public MenuButtonHandler menuButtonHandler;
    
    public Button objectButton1; // 첫 번째 오브젝트 버튼
    public Button objectButton2; // 두 번째 오브젝트 버튼
    public Button objectButton3; // 세 번째 오브젝트 버튼

    public GameObject object1; // 첫 번째 오브젝트
    public GameObject object2; // 두 번째 오브젝트
    public GameObject object3; // 세 번째 오브젝트

    private void Start()
    {
        
        // 초기 상태에서 두 버튼을 비활성화
        objectButton1.gameObject.SetActive(false);
        objectButton2.gameObject.SetActive(false);
        objectButton3.gameObject.SetActive(false);

        // 가방 버튼 클릭 이벤트 등록
        
        objectButton1.onClick.AddListener(() => UseObject(object1));
        objectButton2.onClick.AddListener(() => UseObject(object2));
        objectButton3.onClick.AddListener(()=> UseObject(object3));
    }

    private void ToggleButtons()
    {
        bool isActive = !objectButton1.gameObject.activeSelf;
        objectButton1.gameObject.SetActive(isActive);
        objectButton2.gameObject.SetActive(isActive);
        objectButton3.gameObject.SetActive(isActive);
    }

    private void UseObject(GameObject obj)
    {
        
        menuButtonHandler.ButtonsOn(false);
        
        // 오브젝트 사용 로직을 여기서 구현
        Debug.Log($"{obj.name} 사용!");
        // 예: 오브젝트의 활성화 상태 변경
        obj.SetActive(true);
    }
    

}

