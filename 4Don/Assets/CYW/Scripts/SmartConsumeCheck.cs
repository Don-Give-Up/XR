using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmartConsumeCheck : MonoBehaviour
{
    public Button button1; // 첫 번째 버튼
    public Button button2; // 두 번째 버튼
    public Button button3; // 세 번째 버튼
    public Button button4; // 네 번째 버튼
    public TMP_Text messageText; // 메시지를 표시할 텍스트
    public Sprite checkedImage; // 체크된 이미지
    public Sprite defaultImage; // 기본 이미지

    private void Start()
    {
        // 초기 상태에서 메시지 텍스트를 비활성화
        messageText.gameObject.SetActive(false);

        // 버튼 클릭 이벤트 등록
        button1.onClick.AddListener(() => DisplayMessage(button1));
        button2.onClick.AddListener(() => DisplayMessage(button2));
        button3.onClick.AddListener(() => DisplayMessage(button3));
        button4.onClick.AddListener(() => DisplayMessage(button4));
    }

    private void DisplayMessage(Button clickedButton)
    {
        // 메시지 텍스트를 활성화하고 내용 설정
        messageText.gameObject.SetActive(true);
        messageText.text = "합리적 소비입니다.";

        // 클릭된 버튼의 이미지 변경
        Image buttonImage = clickedButton.GetComponent<Image>();
        buttonImage.sprite = checkedImage;

        // 코루틴 호출
        StartCoroutine(HideMessageAfterDelay(2f));
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        // 주어진 시간만큼 대기
        yield return new WaitForSeconds(delay);
        
        // 메시지 텍스트 비활성화
        messageText.gameObject.SetActive(false);
    }
    
}
